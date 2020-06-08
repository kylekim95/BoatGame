using UnityEngine;

public class Detect : MonoBehaviour
{
    EnemyTest parent;

    private void Awake()
    {
        parent = transform.parent.GetComponent<EnemyTest>();
    }
    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,(other.transform.position - transform.position),out hit))
        {
            if(hit.transform.gameObject.name == "Player")
            {
                Vector3 v1 = (hit.point - transform.position).normalized;
                Vector3 v2 = transform.forward;
                float d = Vector3.Dot(v1, v2);
                if (d >= Mathf.PI / 4 && d <= 3 * Mathf.PI / 4)
                {
                    parent.targetPos = other.transform.position;
                    parent.s = EnemyTest.ai_state.StartTracking;
                }
            }
        }
    }
}
