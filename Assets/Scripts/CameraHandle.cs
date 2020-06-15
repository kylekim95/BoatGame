using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandle : MonoBehaviour
{
    public GameObject thirdCamera;
    public GameObject firstCamera;
    bool changeCamera;



    public void ShowOverView()
    {
        thirdCamera.gameObject.SetActive(true);
        firstCamera.gameObject.SetActive(false);


    }

    public void ShowFirstView()
    {
        firstCamera.gameObject.SetActive(true);
        thirdCamera.gameObject.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {

        changeCamera = false;
        ShowOverView();

    }

    // Update is called once per frame
    void Update()
    {
        /*

        if (Input.GetKeyDown(KeyCode.F5))
        {

            Debug.Log("f5 pressed");

            if (changeCamera)
            {
                ShowOverView();
                changeCamera = false;
            }
            else
            {

                ShowFirstView();

                changeCamera = true;

            }

        }
        */

    }
}
