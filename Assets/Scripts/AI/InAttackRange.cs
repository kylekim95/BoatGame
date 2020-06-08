using UnityEngine;

public class InAttackRange : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, other.transform.position - transform.position, out hit))
        {
            if(hit.collider.gameObject.name == "Player")
            {
                HealthManager.instance.ChangeHealth(-1);
                transform.parent.GetComponent<EnemyTest>().s = EnemyTest.ai_state.Idle;
            }
        }
    }
}
