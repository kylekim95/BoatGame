using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public GameObject doorL;
    public GameObject doorR;
    public static Gate Instance;
    // Start is called before the first frame update


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGate()
    {

        doorL.SetActive(false);
        doorR.SetActive(false);


    }
}
