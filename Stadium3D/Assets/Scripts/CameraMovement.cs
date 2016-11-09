using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    public float rotateSpeed = 500f;

    public GameObject stadium;

    private void Update()
    {
        transform.LookAt(stadium.transform);
        transform.Translate(Vector3.right * Time.deltaTime*rotateSpeed);

        if (Input.touchCount == 1)
        {
            //var touchZero = Input.GetTouch(0);

            //var movementX = touchZero.position.x - touchZero.deltaPosition.x;
            //var movementY = touchZero.position.y - touchZero.deltaPosition.y;

            //var cameraComponent = GetComponent<Camera>();

            //cameraComponent.transform.Translate(-movementX/100, 0, -movementY/100);
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
                cameraComponent.orthographicSize = Mathf.Max(cameraComponent.orthographicSize, 15f);
            }
            else
            {
                cameraComponent.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, 15f, 79.9f);
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
