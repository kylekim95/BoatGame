﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

public class EnemyAI : FiniteStateMachine
{
    public FieldOfView fov;

    public Pathfinder pathfinder;
    public Vector3 targetPos;
    List<Vector3> path;
    int curTarget;
    public float moveSpeed = 3f;

    public GameObject attackTarget;
    public float attackDelay = 1f;
    public float attackDistance = 3f;
    public float attackCoolDown = 3f;

    protected override void Awake()
    {
        if (pathfinder == null)
        {
            this.enabled = false;
            return;
        }
        base.Awake();

        states.Add("StartChase", states.Count);
        stateActions.Add(StateAction_StartChase);

        states.Add("Chase", states.Count);
        stateActions.Add(StateAction_Chase);

        states.Add("Search", states.Count);
        stateActions.Add(StateAction_Search);

        states.Add("StartAttack", states.Count);
        stateActions.Add(StateAction_StartAttack);

        states.Add("Attack", states.Count);
        stateActions.Add(StateAction_Attack);
    }

    void StateAction_StartChase()
    {
        path = pathfinder.FindPath(transform.position, targetPos);
        if (path == null)
            ChangeState("Idle");
        else
        {
            curTarget = 0;
            ChangeState("Chase");
        }
    }
    void StateAction_Chase()
    {
        Vector3 dir = path[curTarget] - transform.position;
        dir.y = 0;
        if (dir.magnitude < 0.05f)
            curTarget++;

        if (curTarget < path.Count)
        {
            dir = dir.normalized;
            transform.position += (dir * moveSpeed * Time.deltaTime);
            transform.LookAt(transform.position + dir);
        }
        else
        {
            ChangeState("Search");
        }
    }
    void StateAction_Search()
    {
        ChangeState("Idle");
    }
    void StateAction_StartAttack()
    {
        fov.trigOn = false;
        StartCoroutine("AttackCoroutine");
        ChangeState("Attack");
    }
    void StateAction_Attack()
    {

    }

    IEnumerator AttackCoroutine()
    {
        float timer = 1f;
        while (timer > 0)
        {
            transform.LookAt(attackTarget.transform.position);
            timer -= Time.deltaTime;
            yield return null;
        }

        Vector3 finalPos = transform.position + transform.forward * attackDistance;
        while((finalPos - transform.position).magnitude > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, finalPos, 0.125f);
            yield return null;
        }
        fov.trigOn = true;

        StartCoroutine("CoolDownTimer", attackCoolDown);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
