
using System;
using UnityEngine;

public class Node : IComparable, IEquatable<Node>
{
    public Vector3 worldPos
    {
        get
        {
            return _worldPos;
        }
    }
    public Vector2Int gridPos
    {
        get
        {
            return _gridPos;
        }
    }
    Vector3 _worldPos;
    Vector2Int _gridPos;

    public bool walkable;
    public float gCost, hCost;
    public float fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    public Node parent;
    
    public Node(Vector3 wp, Vector2Int gp)
    {
        _worldPos = wp;
        _gridPos = gp;
        gCost = hCost = 0;
        parent = this;
        walkable = true;
    }

    public int CompareTo(object obj)
    {
        return fCost.CompareTo(((Node)obj).fCost);
    }
    public bool Equals(Node other)
    {
        return _gridPos.Equals(other.gridPos);
    }
}
