using UnityEngine;

public class Waves : MonoBehaviour
{
    Material mat;
    public float wavelength = 10f;
    public float steepness = 1f;
    public Vector2 direction;

    public Transform player;
    Vector3 prevPlayerPos;
    float maxDuration = 1;
    bool ripple = false;
    float timer = 0;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        prevPlayerPos = player.position - transform.forward;
    }
    private void Update()
    {
        mat.SetVector("_WaveA", new Vector4(direction.x, direction.y, steepness, wavelength));
        if (ripple)
        {
            timer += Time.deltaTime;
            mat.SetFloat("_TimeSinceRippleStart1", timer);
            if(timer > maxDuration)
            {
                ripple = false;
            }
        }
        else if (prevPlayerPos != player.position-transform.forward && !ripple)
        {
            StartRipple();
        }
        prevPlayerPos = player.position - transform.forward;
    }
    void StartRipple()
    {
        mat.SetVector("_RippleOrigin1", prevPlayerPos);
        mat.SetFloat("_MaxDuration", maxDuration);
        ripple = true;
        timer = 0;
    }
}
