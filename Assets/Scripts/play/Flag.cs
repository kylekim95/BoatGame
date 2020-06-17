using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
   
    public float waterHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.y < waterHeight)
        {
            UI.instance.playerInstance.Damage(100);
        }
    }
}
