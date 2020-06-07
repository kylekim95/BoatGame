using System;
using System.Data.SqlTypes;
using UnityEngine;

public class BinHeap<T> where T : IComparable
{
    T[] heap;
    int nextInsLeaf = 0;
    public bool empty
    {
        get
        {
            return (nextInsLeaf == 0);
        }
    }

    public BinHeap(int nNodes)
    {
        heap = new T[nNodes];
    }

    void Swap(int ind0, int ind1)
    {
        T temp = heap[ind0];
        heap[ind0] = heap[ind1];
        heap[ind1] = temp;
    }
    public void Insert(T val)
    {
        if (nextInsLeaf >= heap.Length)
            return;

        int insertPos = nextInsLeaf;
        nextInsLeaf++;
        heap[insertPos] = val;
        int parentInd = (insertPos - 1) / 2;
        while (insertPos >= 0 && heap[insertPos].CompareTo(heap[(insertPos - 1) / 2]) < 0)
        {
            Swap(insertPos, parentInd);
            insertPos = parentInd;
            parentInd = (insertPos - 1) / 2;
        }
    }
    public T Pop()
    {
        T ret = heap[0];

        heap[0] = heap[nextInsLeaf - 1];
        heap[nextInsLeaf - 1] = default(T);
        nextInsLeaf--;

        int curInd = 0;
        while (curInd < (nextInsLeaf - 2) / 2)
        {
            int swapTarget = curInd;
            if (heap[curInd].CompareTo(heap[2 * curInd + 1]) >= 0)
            {
                swapTarget = 2 * curInd + 1;
            }
            if (heap[swapTarget].CompareTo(heap[2 * curInd + 2]) >= 0)
            {
                swapTarget = 2 * curInd + 2;
            }
            if (swapTarget == curInd)
            {
                break;
            }
            Swap(swapTarget, curInd);
            curInd = swapTarget;
        }
        return ret;
    }
    public bool Contains(T t)
    {
        for(int i = 0; i < nextInsLeaf; i++)
        {
            if (heap[i].Equals(t))
                return true;
        }
        return false;
    }

    public override string ToString()
    {
        string ret = "";
        for (int i = 0; i < nextInsLeaf; i++)
            ret += (heap[i].ToString() + "  ");
        return ret;
    }
}