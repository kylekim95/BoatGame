using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projSpeed = 5f;
    public LayerMask hits;

    private void Update()
    {
        transform.position += transform.forward * projSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hits == (hits | 1 << other.gameObject.layer))
        {
            Debug.Log("Hit!" + other.gameObject.name);
            Destroy(gameObject);
        }
    }
}
