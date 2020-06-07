using UnityEngine;

public class Outlineable : MonoBehaviour
{
    public Color outlineColor;

    private void Update()
    {
        GetComponent<Renderer>().material.SetOverrideTag("Outline", "True");
        GetComponent<Renderer>().material.SetColor("_OutlineColor", outlineColor);
    }
}
