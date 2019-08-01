using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbitImproved : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public float zoomSenstivity = 0.1f;

    float x = 0.0f;
    float y = 0.0f;
    [SerializeField]
    float zoomFactor = 0.0f;

    public KeyCode desiredKey = KeyCode.LeftControl;
    public int mouseButtonIndex = 0;

    public bool useZoom = true;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        this.distance = 5f;
    }

    void LateUpdate()
    {

        if (target)
        {
            if (Input.GetMouseButton(1) & Input.GetKey(KeyCode.LeftControl) & this.useZoom)
            {
                zoomFactor = Input.GetAxis("Mouse Y") * this.zoomSenstivity*Time.deltaTime;
            }
            else
            {
                this.zoomFactor = 0;
            }

            if (Input.GetMouseButton(this.mouseButtonIndex) & Input.GetKey(this.desiredKey))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * Time.deltaTime;
                y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - this.zoomFactor * 5, distanceMin, distanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}