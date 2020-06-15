using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Grid grid;
    public float diagCost = 1.5f;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    float calc_hCost(Vector2Int from, Vector2 to)
    {
        float x = Mathf.Abs(to.x - from.x);
        float z = Mathf.Abs(to.y - from.y);
        if (x > z)
            return diagCost * z + (x - z);
        return diagCost * x + (z - x);
    }
    public List<Vector3> FindPath(Vector3 from, Vector3 to)
    {
        Node start = grid.GetNode(from);
        Node finish = grid.GetNode(to);
        List<Vector3> path = new List<Vector3>(10);
        BinHeap<Node> open = new BinHeap<Node>(100);
        List<Node> closed = new List<Node>();

        open.Insert(start);
        while (!open.empty)
        {
            Node cur = open.Pop();
            closed.Add(cur);
            if (cur.Equals(finish))
            {
                while(cur != start)
                {
                    path.Add(cur.worldPos);
                    cur = cur.parent;
                }
                path.Reverse();
                return path;
            }
            foreach(Node n in grid.GetAdjNodes(cur))
            {
                if (!n.walkable || closed.Contains(n))
                    continue;
                float cost = 1f;
                if (n.gridPos.x != cur.gridPos.x && n.gridPos.y != cur.gridPos.y)
                    cost = diagCost;
                float gCost = cost + cur.gCost;

                bool contains = open.Contains(n);
                if (!contains || gCost < n.gCost)
                {
                    n.gCost = gCost;
                    n.hCost = calc_hCost(n.gridPos, finish.gridPos);
                    n.parent = cur;
                    if (!contains)
                    {
                        open.Insert(n);
                    }
                }
            }
        }
        return path;
    }
}
