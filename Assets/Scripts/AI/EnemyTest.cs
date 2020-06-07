using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = .005f;

    public enum AIState
    {
        Idle = 0,
        StartTracking,
        Track,
    }
    public AIState state;

    void IdleSequence()
    {

    }

    Stack<Vector3> path;
    Vector3 curTarget;
    float timer;
    void TrackingSequence()
    {
        if (timer > 1f)
        {
            if (!Grid.instance.Walkable(curTarget))
            {
                path = null;
                state = AIState.StartTracking;
                return;
            }
            transform.position = curTarget;
            if (path.Count <= 0)
            {
                path = null;
                state = AIState.Idle;
                return;
            }
            curTarget = path.Pop();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    private void Awake()
    {
        state = AIState.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case AIState.Idle:
                IdleSequence();
                break;
            case AIState.StartTracking:
                path = AStarPathFinder.instance.FindPath(transform.position, target.position);
                if (path.Count <= 0)
                {
                    path = null;
                    state = AIState.Idle;
                    return;
                }
                curTarget = path.Pop();
                timer = 0;
                state = AIState.Track;
                break;
            case AIState.Track:
                TrackingSequence();
                break;
        }
    }
}
