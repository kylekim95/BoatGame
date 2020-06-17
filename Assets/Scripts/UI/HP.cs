using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private Image Hp;
    

    public Vector2 HealthSize = new Vector2(64, 64);
    // Start is called before the first frame update
    void Awake()
    {
        Hp = GetComponent<Image>();

        Hp.rectTransform.sizeDelta = HealthSize;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ChangeHearth(int h)
    {
        if (h < 0) h = 0;

        HealthSize.x = 64*h;
        Hp.rectTransform.sizeDelta = HealthSize;
    }


}
