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
                parent.targetPos = other.transform.position;
                parent.s = EnemyTest.ai_state.StartTracking;
            }
        }
    }
}
