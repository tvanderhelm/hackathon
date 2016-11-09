using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    public float rotateSpeed = 10f;
    public float minZoomLevel = 15f;
    public float maxZoomLevel = 80f;
    public float touchRotateMultiplier = 2f;

    private Vector3 point;

    public GameObject stadium;

    private void Start()
    {
        point = stadium.transform.position;
        transform.LookAt(point);
    }

    private void Update()
    {
        if (Input.touchCount < 2)
        {
            float rotateMultiplier = 1f;

            if (Input.touchCount == 1)
            {
                var touchZero = Input.GetTouch(0);

                Debug.Log(touchZero.deltaPosition);

                rotateMultiplier = touchZero.deltaPosition.x * touchRotateMultiplier;
            }

            transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * rotateSpeed * rotateMultiplier);
        }
        else if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);

            var touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            var touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            var prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            var touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            var deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            var cameraComponent = GetComponent<Camera>();

            if (cameraComponent.orthographic)
            {
                cameraComponent.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
                cameraComponent.orthographicSize = Mathf.Max(cameraComponent.orthographicSize, minZoomLevel);
            }
            else
            {
                cameraComponent.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, minZoomLevel, maxZoomLevel);
            }
        }
    }

    // convert screen point to world point
    private Vector2 getWorldPoint(Vector2 screenPoint)
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
        return hit.point;
    }
}
