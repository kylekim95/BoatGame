using UnityEngine;

public class OutlineCam : MonoBehaviour
{
    public Shader outlineGen;
    Material outlineGenMat;
    [Range(0,10)]
    public int downSampleIter = 0;
    [Range(0, 10)]
    public int blurIter = 0;

    private void Awake()
    {
        if(outlineGen == null)
        {
            this.enabled = false;
            return;
        }
        outlineGenMat = new Material(outlineGen);
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //DOWNSAMPLE SOURCE
        RenderTexture temp = RenderTexture.GetTemporary(source.descriptor);
        Graphics.Blit(source, temp);
        int width = temp.width;
        int height = temp.height;
        for(int i = 0; i < downSampleIter; i++)
        {
            width /= 2;
            height /= 2;
            RenderTexture temp2 = RenderTexture.GetTemporary(width / 2, height / 2);
            Graphics.Blit(temp, temp2);
            RenderTexture.ReleaseTemporary(temp);
            temp = temp2;
        }
        RenderTexture temp3 = RenderTexture.GetTemporary(source.descriptor);
        Graphics.Blit(temp, temp3);
        RenderTexture.ReleaseTemporary(temp);
        temp = temp3;

        //SUBTRACT SOURCE FROM BLURRED
        outlineGenMat.SetTexture("_SubtractTex", source);
        RenderTexture temp4 = RenderTexture.GetTemporary(temp.descriptor);
        for (int i = 0; i < blurIter; i++)
        {
            Graphics.Blit(temp, temp4, outlineGenMat);
            RenderTexture.ReleaseTemporary(temp);
            temp = temp4;
        }
        //OUT
        Graphics.Blit(temp, destination);
        RenderTexture.ReleaseTemporary(temp);
    }
}
