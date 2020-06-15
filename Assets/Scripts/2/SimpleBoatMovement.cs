using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBoatMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float rotSpeed = 10f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -rotSpeed, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, rotSpeed, 0));
        }
        if (Input.GetKey(KeyCode.Space))
        {
            HealthManager.instance.ChangeHealth(-1);
        }
    }
}
