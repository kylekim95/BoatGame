using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] obstacles;


    public void Awake()
    {
        
        //obstacle = GetComponent<GameObject>();
    }


    void Start()
    {
        
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody py = other.gameObject.GetComponent<Rigidbody>();

        Vector3 dir = (py.transform.position - transform.position).normalized;

        py.AddForce(dir * 50, ForceMode.Impulse);

        //Rigidbody rb = other.GetComponentInParent<Rigidbody>();

        //Vector3 f  = (rb.transform.position - obstacle.transform.position).normalized;

        //rb.AddForce(10*f, ForceMode.Impulse);
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.GetContact(0).otherCollider;
        Player player = other.gameObject.GetComponent<Player>();

        if (player.mortal)
        {
            
            Rigidbody py = other.gameObject.GetComponent<Rigidbody>();


            Vector3 dir = (py.transform.position - transform.position).normalized;

            py.AddForce(dir, ForceMode.Impulse);

            player.Damage(1);
            player.StartCoroutine(player.BeImmortal(3));

            //player.mortal = true;
        }
    }



}
