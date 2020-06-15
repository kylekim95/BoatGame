using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool gotIt;
    // Start is called before the first frame update
    void Start()
    {
        gotIt = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            gotIt = true;
            gameObject.SetActive(false);
            Gate.Instance.OpenGate();
        }
    }
}
