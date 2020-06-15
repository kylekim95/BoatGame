using System;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [Serializable]
    public class WaveDesc
    {
        public Vector2 direction;
        [Range(0,1)]
        public float steepness;
        public float wavelength;
    }
    [SerializeField]
    List<WaveDesc> waves;

    Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        for(int i = 0; i < waves.Count; i++)
        {
            mat.SetVector("_Wave" + i.ToString(), new Vector4(waves[i].direction.x, waves[i].direction.y, waves[i].steepness, waves[i].wavelength));
        }
    }
}
