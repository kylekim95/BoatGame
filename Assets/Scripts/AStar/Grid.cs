using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    Node[,] grid;
    float nodeSizeX, nodeSizeZ;

    public Vector3 topLeft, bottomRight;
    public int nNodeX, nNodeZ;
    public LayerMask unwalkable;

    bool initValidation()
    {
        if (topLeft.x < bottomRight.x &&
            topLeft.z > bottomRight.z &&
            nNodeX > 0 && nNodeZ > 0
           )
            return true;
        return false;
    }
    void initGrid()
    {
        nodeSizeX = (bottomRight.x - topLeft.x) / nNodeX;
        nodeSizeZ = (topLeft.z - bottomRight.z) / nNodeZ;

        grid = new Node[nNodeX, nNodeZ];    
        for(int i = 0; i < nNodeX; i++)
        {
            for (int j = 0; j < nNodeZ; j++)
            {
                grid[i, j] = new Node(new Vector3(topLeft.x + nodeSizeX * i, 0, bottomRight.z + nodeSizeZ * j), new Vector2Int(i,j));
                Collider[] col = Physics.OverlapBox(grid[i, j].wPos, new Vector3(nodeSizeX / 2, 1, nodeSizeZ / 2), Quaternion.identity, unwalkable);
                if (col.Length > 0)
                    grid[i, j].walkable = false;
            }
        }
    }
    private void Awake()
    {
        instance = this;
        if (!initValidation())
        {
            this.enabled = false;
            return;
        }
        initGrid();
    }

    void reCalcWalkable(Vector3 wPos)
    {
        for(int i = -2; i < 3; i++)
        {
            for (int j = -2; j < 3; j++)
            {
                Node temp = GetNode(wPos + new Vector3(i* nodeSizeX, 0, j*nodeSizeZ));
                if (temp == null)
                    continue;
                Collider[] col = Physics.OverlapBox(temp.wPos, new Vector3(nodeSizeX / 2, 1, nodeSizeZ / 2), Quaternion.identity, unwalkable);
                if (col.Length > 0)
                    temp.walkable = false;
                else
                    temp.walkable = true;
            }
        }
    }
    bool wPosValidation(Vector3 wPos)
    {
        if (wPos.x >= topLeft.x && wPos.x <= bottomRight.x &&
            wPos.z >= bottomRight.z && wPos.z <= topLeft.z
            )
            return true;
        return false;
    }
    public Node GetNode(Vector3 wPos)
    {
        if (!wPosValidation(wPos))
            return null;
        int x = Mathf.Clamp(Mathf.RoundToInt((wPos.x - topLeft.x) / nodeSizeX), 0, nNodeX-1);
        int z = Mathf.Clamp(Mathf.RoundToInt((wPos.z - bottomRight.z) / nodeSizeZ), 0, nNodeZ-1);
        return grid[x, z];
    }
    public bool Walkable(Vector3 wPos)
    {
        Collider[] col = Physics.OverlapBox(wPos, new Vector3(nodeSizeX / 2, 1, nodeSizeZ / 2), Quaternion.identity, unwalkable);
        if (col.Length > 0)
        {
            if (GetNode(wPos).walkable)
                reCalcWalkable(wPos);
            return false;
        }
        return true;
    }
    public List<Node> GetAdjacentNodes(Node n)
    {
        Vector2Int nGridPos = n.gridPos;
        List<Node> adjNodes = new List<Node>();
        for(int i = -1; i < 2; i++)
        {
            if (nGridPos.x + i < 0 || nGridPos.x + i >= nNodeX) continue;
            for(int j = -1; j < 2; j++)
            {
                if (nGridPos.y + j < 0 || nGridPos.y + j >= nNodeZ) continue;
                else
                {
                    adjNodes.Add(grid[nGridPos.x + i, nGridPos.y + j]);
                }
            }
        }
        return adjNodes;
    }
}
