using UnityEngine;

public class InAttackRange : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, other.transform.position - transform.position, out hit))
            {

            }
        }
    }
}
