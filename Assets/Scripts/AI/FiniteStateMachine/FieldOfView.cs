using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    float FOV_radius;
    public float FOV_width;
    public LayerMask raycast;
    public float attackRadius;
    EnemyAI ai;
    public bool trigOn = true;

    private void Awake()
    {
        FOV_radius = GetComponent<SphereCollider>().radius;
    }
    private void Start()
    {
        ai = GetComponentInParent<EnemyAI>();
        if(ai == null)
        {
            this.enabled = false;
            return;
        }
        ai.fov = this;
    }

    private void OnTriggerStay(Collider other)
    {
        if (trigOn)
        {
            Vector3 dir = (other.transform.position - transform.position).normalized;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, FOV_radius, raycast))
            {
                if (hit.transform.gameObject.name == "Player")
                {
                    float dot = Vector3.Dot(dir, transform.forward);
                    if (dot > Mathf.Cos(FOV_width * Mathf.Deg2Rad) || hit.distance < 1)
                    {
                        if (hit.distance < attackRadius)
                        {
                            ai.attackTarget = hit.collider.gameObject;
                            ai.ChangeState("StartAttack");
                        }
                        else
                        {
                            ai.targetPos = hit.point;
                            ai.ChangeState("StartChase");
                        }
                    }
                }
            }
        }
    }
}
