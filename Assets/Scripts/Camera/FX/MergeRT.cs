using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MergeRT : MonoBehaviour
{
    [Serializable]
    public class MergeSet
    {
        public RenderTexture mergeCamRT;
        public Shader merge;
    }

    public List<MergeSet> mergeSets;
    List<MergeSet> validMergeSet;

    private void Start()
    {
        validMergeSet = new List<MergeSet>();

        foreach(MergeSet ms in mergeSets)
        {
            if (ms.mergeCamRT != null && ms.merge != null)
            {
                validMergeSet.Add(ms);
            }
        }
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture temp = RenderTexture.GetTemporary(source.descriptor);
        Graphics.Blit(source, temp);

        foreach(MergeSet ms in validMergeSet)
        {
            Material mergeMat = new Material(ms.merge);
            mergeMat.SetTexture("_MergeRT",ms.mergeCamRT);
            Graphics.Blit(temp, temp, mergeMat);
        }

        Graphics.Blit(temp, destination);
        RenderTexture.ReleaseTemporary(temp);
    }
}
