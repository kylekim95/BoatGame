using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SetReplShader : MonoBehaviour
{
    public Shader replShader;
    public string tag = "";

    private void OnEnable()
    {
        //Repl Shader for outline fx
        if(replShader != null)
        {
            GetComponent<Camera>().SetReplacementShader(replShader, tag);
        }
    }
    private void OnDisable()
    {
        GetComponent<Camera>().ResetReplacementShader();
    }
}
