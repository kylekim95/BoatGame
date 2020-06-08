using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public Pathfinder pathfinder;
    public Vector3 targetPos;
    public enum ai_state
    {
        Idle = 0,
        StartTracking,
        Tracking,
        Search,
        Attack
    }
    public ai_state s;

    List<Vector3> path;
    int curObj;
    public float moveSpeed = 1f;

    void Idle()
    {

    }
    void StartTracking()
    {
        path = pathfinder.FindPath(transform.position, targetPos);
        curObj = 0;
        s = ai_state.Tracking;
    }
    void Tracking()
    {
        if(curObj >= path.Count)
        {
            s = ai_state.Search;
            return;
        }
        Vector3 dir = path[curObj] - transform.position;
        if (dir.magnitude < .05f)
            curObj++;
        else
        {
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;
            transform.LookAt(path[curObj]);
        }
    }
    void Search()
    {
        s = ai_state.Idle;
    }
    void Attack()
    {

    }

    private void Awake()
    {
        s = ai_state.Idle;
    }
    private void Update()
    {
        switch (s)
        {
            case ai_state.Idle:
                Idle();
                break;
            case ai_state.StartTracking:
                StartTracking();
                break;
            case ai_state.Tracking:
                Tracking();
                break;
            case ai_state.Search:
                Search();
                break;
            case ai_state.Attack:
                Attack();
                break;
        }
    }
}
