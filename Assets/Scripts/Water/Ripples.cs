using System.Collections.Generic;
using UnityEngine;

public class Ripples : MonoBehaviour
{
    public class RippleDesc
    {
        public bool inUse = false;
        public float timeSinceStart = 0;
        public RippleDesc() { }
    }
    List<RippleDesc> ripples;
    public int numRipples = 3;
    public float rippleMaxDur = 1f;
    public float rippleDelay = .5f;

    public List<GameObject> dynObjects;
    List<Vector3> prevPos_dynObjects;
    List<int> lastRippleInd;

    Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_RippleMaxDuration", rippleMaxDur);
        ripples = new List<RippleDesc>(numRipples);
        for(int i = 0; i < numRipples; i++)
        {
            ripples.Add(new RippleDesc());
        }
    }
    private void Start()
    {
        prevPos_dynObjects = new List<Vector3>(dynObjects.Count);
        lastRippleInd = new List<int>(dynObjects.Count);
        for (int i = 0; i < dynObjects.Count; i++)
        {
            prevPos_dynObjects.Add(dynObjects[i].transform.position);
            lastRippleInd.Add(-1);
        }
    }
    private void Update()
    {
        for(int i = 0; i < dynObjects.Count; i++)
        {
            if(dynObjects[i].transform.position != prevPos_dynObjects[i])
            {
                if (lastRippleInd[i] == -1 || ripples[lastRippleInd[i]].timeSinceStart > rippleDelay)
                {
                    StartRipple(dynObjects[i].transform.position, i);
                }
                prevPos_dynObjects[i] = dynObjects[i].transform.position;
            }
        }
        for (int i = 0; i < numRipples; i++)
        {
            if (!ripples[i].inUse)
                continue;
            if (ripples[i].timeSinceStart > rippleMaxDur)
                ripples[i].inUse = false;
            else
            {
                ripples[i].timeSinceStart += Time.deltaTime;
                mat.SetFloat("_Ripple" + i.ToString() + "Time", ripples[i].timeSinceStart);
            }
        }
    }

    void StartRipple(Vector3 origin, int objInd)
    {
        for(int i = 0; i < numRipples; i++)
        {
            if (ripples[i].inUse)
                continue;
            ripples[i].inUse = true;
            mat.SetVector("_Ripple" + i.ToString() + "Origin", origin);
            ripples[i].timeSinceStart = 0;
            lastRippleInd[objInd] = i;
            break;
        }
    }
}
