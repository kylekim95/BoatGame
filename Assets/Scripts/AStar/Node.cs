using System;
using System.Data.SqlTypes;
using UnityEngine;

public class Node : IComparable
{
    Vector3 _wPos;
    Vector2Int _gridPos;
    public bool walkable;
    public Vector3 wPos
    {
        get
        {
            return _wPos;
        }
    }
    public Vector2Int gridPos {
        get
        {
            return _gridPos;
        }
    }

    public Node parent = null;
    public float gCost;
    public float hCost;
    public float fCost;

    public Node(Vector3 wp, Vector2Int gp)
    {
        _wPos = wp;
        _gridPos = gp;
        walkable = true;
    }
    public int CompareTo(object n)
    {
        return fCost.CompareTo(((Node)n).fCost);
    }

    public override string ToString()
    {
        return _wPos + ", " + _gridPos;
    }
}
