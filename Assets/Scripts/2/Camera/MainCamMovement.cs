using UnityEngine;

public class MainCamMovement : MonoBehaviour
{
    public Transform target;
    bool cursorLocked;
    public float radius = 3f;

    public float curTheta = 0;
    public float curPhi = 0;
    public float dir = 1;

    public Vector3 newTarget;

    private void Awake()
    {
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
            curTheta += dir * mouseDispDir.y * 0.05f;
            curTheta = Mathf.Clamp(curTheta, 0.001f, Mathf.PI - 0.001f);

            //Spherical Coordinates
            Vector3 sphericalCoord = new Vector3(
                    radius * Mathf.Sin(curTheta) * Mathf.Cos(-curPhi),
                    radius * Mathf.Cos(curTheta),
                    radius * Mathf.Sin(curTheta) * Mathf.Sin(-curPhi)
                );

            transform.position = Vector3.Lerp(transform.position, target.position + new Vector3(0,10,-10) + sphericalCoord, 0.125f);
            transform.LookAt(target.position);
        }
    }
}

