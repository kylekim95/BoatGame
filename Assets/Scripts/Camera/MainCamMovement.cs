using UnityEngine;

public class MainCamMovement : MonoBehaviour
{
    public Transform target;
    bool cursorLocked;
    public float maxRadius = 3f;
    public float radius = 3f;

    public float curTheta = 0;
    public float curPhi = 0;

    Vector3 camVel;
    public LayerMask everything;

    private void Awake()
    {
        radius = maxRadius;
        Debug.Log("Press ESC to unlock cursor");
        if (target == null)
        {
            this.enabled = false;
            return;
        }
        cursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (cursorLocked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;

            Cursor.visible = cursorLocked;
            cursorLocked = !cursorLocked;
        }
    }
    private void LateUpdate()
    {
        if (cursorLocked)
        {
            Vector2 mouseDispDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).normalized;

            curPhi += mouseDispDir.x * 0.05f;
            curTheta += mouseDispDir.y * 0.05f;
            curTheta = Mathf.Clamp(curTheta, 0.001f, Mathf.PI/3);

            //Spherical Coordinates
            Vector3 sphericalCoord = new Vector3(
                    radius * Mathf.Sin(curTheta) * Mathf.Cos(-curPhi),
                    radius * Mathf.Cos(curTheta),
                    radius * Mathf.Sin(curTheta) * Mathf.Sin(-curPhi)
                );
            Vector3 finPos = Vector3.SmoothDamp(transform.position, target.position + sphericalCoord, ref camVel, 0.05f, 30f);
            
            Collider[] cols = Physics.OverlapSphere(finPos, 1, everything);
            if (cols.Length <= 0)
                radius = Mathf.Lerp(radius, maxRadius, 0.1f);
            else
            {
                foreach (Collider col in cols)
                {
                    Vector3 dir = col.ClosestPoint(finPos) - finPos;
                    float mag = 1 - dir.magnitude;
                    finPos -= dir * mag;
                    radius = (finPos - target.position).magnitude;
                }
            }

            transform.position = finPos;
            transform.LookAt(target.position);
        }
    }
}

