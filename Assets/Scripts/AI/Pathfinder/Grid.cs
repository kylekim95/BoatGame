using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Vector2 topLeft, bottomRight;
    public int nNodeX, nNodeZ;
    float nodeDiamX, nodeDiamZ;
    Node[,] grid;

    private void Awake()
    {
        nodeDiamX = (bottomRight.x - topLeft.x) / nNodeX;
        nodeDiamZ = (topLeft.y - bottomRight.y) / nNodeZ;

        grid = new Node[nNodeX, nNodeZ];
        for (int i = 0; i < nNodeX; i++)
        {
            for (int j = 0; j < nNodeZ; j++)
            {
                Vector3 wp = new Vector3(
                    topLeft.x + nodeDiamX / 2 + nodeDiamX * i,
                    0,
                    bottomRight.y + nodeDiamZ / 2 + nodeDiamZ * j
                  );
                grid[i, j] = new Node(wp, new Vector2Int(i, j));
                grid[i, j].walkable = !Physics.CheckBox(wp, new Vector3(nodeDiamX / 2, 0.5f, nodeDiamZ / 2), Quaternion.identity, LayerMask.GetMask("Obstacle"));
            }
        }
    }

    public List<Node> GetAdjNodes(Node n)
    {
        List<Node> adjNodes = new List<Node>(10);
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                int xind = n.gridPos.x + i;
                int zind = n.gridPos.y + j;

                if (xind >= 0 && xind < nNodeX && zind >= 0 && zind < nNodeZ)
                    adjNodes.Add(grid[xind, zind]);
            }
        }
        return adjNodes;
    }
    public Node GetNode(Vector3 wp)
    {
        float percentX = (wp.x + (bottomRight.x - topLeft.x) / 2) / (bottomRight.x - topLeft.x);
        float percentY = (wp.z + (topLeft.y - bottomRight.y) / 2) / (topLeft.y - bottomRight.y);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((nNodeX - 1) * percentX);
        int y = Mathf.RoundToInt((nNodeZ - 1) * percentY);

        if (x >= nNodeX || x < 0 || y < 0 || y >= nNodeZ)
            return null;

        return grid[x, y];
    }
}
