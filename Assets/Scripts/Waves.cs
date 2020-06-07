using UnityEngine;

public class Waves : MonoBehaviour
{
    Material mat;
    public float wavelength = 10f;
    public float steepness = 1f;
    public Vector2 direction;

    public GameObject player;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        mat.SetVector("_WaveA", new Vector4(direction.x, direction.y, steepness, wavelength));
        mat.SetVector("_WaveB", new Vector4(1, 2, steepness, wavelength));
        if (player != null)
        {
            Vector2 d = direction.normalized;
            float k = 2f * 3.1415f / wavelength;
            float c = Mathf.Sqrt(9.8f / k);
            float f = k * (Vector3.Dot(d, new Vector3(player.transform.position.x, player.transform.position.z, 0)) - c * Time.realtimeSinceStartup);
            float a = steepness / k;

            player.transform.position = new Vector3(player.transform.position.x, a * Mathf.Sin(f), player.transform.position.z);
        }
    }
}
