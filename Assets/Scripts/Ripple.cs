using System.Collections;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        
    }
}
