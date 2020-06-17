using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float projSpeed = 5f;

    private void Update()
    {
        transform.position += transform.forward * projSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!");
        Destroy(gameObject);
    }
}
