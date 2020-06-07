using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class AStarPathFinder : MonoBehaviour
{
    float diagCost = 1.2f;

    public Grid grid;
    public static AStarPathFinder instance;

    private void Awake()
    {
        instance = this;
    }

    bool isDiagonal(Vector2Int gp1, Vector2Int gp2)
    {
        if (gp1.x == gp2.x || gp1.y == gp2.y)
            return false;
        return true;
    }
    float calcHCost(Vector2Int from, Vector2Int to)
    {
        int xdiff = Mathf.Abs(to.x - from.x);
        int zdiff = Mathf.Abs(to.y - from.y);
        int larger, smaller;
        if (xdiff > zdiff)
        {
            larger = xdiff;
            smaller = zdiff;
        }
        else
        {
            larger = zdiff;
            smaller = xdiff;
        }
        return diagCost * smaller + (larger - smaller);
    }
    public Stack<Vector3> FindPath(Vector3 from, Vector3 to)
    {
        Node start = grid.GetNode(from);
        Node finish = grid.GetNode(to);

        BinHeap<Node> open = new BinHeap<Node>(100);
        List<Node> closed = new List<Node>();

        Stack<Vector3> path = new Stack<Vector3>();

        start.gCost = 0;
        start.hCost = calcHCost(start.gridPos, finish.gridPos);
        start.fCost = start.gCost + start.hCost;
        open.Insert(start);

        Node cur;
        while((cur = open.Pop()).gridPos != finish.gridPos)
        {
            closed.Add(cur);

            List<Node> adjNodes = grid.GetAdjacentNodes(cur);
            foreach(Node n in adjNodes)
            {
                if (!n.walkable || closed.Contains(n))
                    continue;
                float gCost = 0;
                float cost = 1;
                if (isDiagonal(cur.gridPos, n.gridPos))
                    cost = diagCost;
                gCost += cost + cur.gCost;

                bool contain;
                if(!(contain = open.Contains(n)) || gCost < n.gCost)
                {
                    n.hCost = calcHCost(n.gridPos, finish.gridPos);
                    n.gCost = gCost;
                    n.fCost = n.hCost + n.gCost;
                    n.parent = cur;
                    if (!contain)
                        open.Insert(n);
                }
            }
            if (open.empty)
                return path;
        }
        while(cur.parent != null)
        {
            path.Push(cur.wPos);
            cur = cur.parent;
        }

        foreach(Node n in closed)
        {
            n.gCost = 0;
            n.parent = null;
        }
        while(!open.empty)
        {
            Node temp = open.Pop();
            temp.gCost = 0;
            temp.parent = null;
        }
        return path;
    }
}
