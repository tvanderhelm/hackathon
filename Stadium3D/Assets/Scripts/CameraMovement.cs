using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool rotating = true;
    public bool rotatingToStart = false;
    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    public float rotateSpeed = 10f;
    public float minZoomLevel = 15f;
    public float maxZoomLevel = 80f;
    public float touchRotateMultiplier = 2f;
    public float zoomToFieldMultiplier = 8f;
    public float zoomToFieldOfView = -0.5f;
    public float rotatingToStartMultiplier = 20f;

    public bool zoomtoField;

    private Vector3 point;

    public GameObject stadium;
    public Camera cameraComponent;
    private Animator fieldAnimation;

    public float lerpSpeed = 50f;
    private Vector3 lerpTarget;
    private float startTime;
    private float journeyLength;
    private bool lerpStarted;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
        point = stadium.transform.position;
        transform.LookAt(point);
    }

    private void Update()
    {
        if (lerpStarted)
        {
            var distCovered = (Time.time - startTime) * lerpSpeed;
            var fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, lerpTarget, fracJourney);

            if (lerpTarget.x - 1.0f <= transform.position.x && lerpTarget.x + 1.0f >= transform.position.x && lerpTarget.y - 1.0f <= transform.position.y && lerpTarget.y + 1.0f >= transform.position.y && lerpTarget.z - 1.0f <= transform.position.z && lerpTarget.z + 1.0f >= transform.position.z)
            {
                lerpStarted = false;
            }
        }
        else if (zoomtoField)
        {
            transform.RotateAround(point, new Vector3(-1.0f, 0.0f, 0.0f), 20 * Time.deltaTime * rotateSpeed * zoomToFieldMultiplier);
            if (transform.position.y > 8000 && transform.position.y < 11000)
            {
                Debug.Log(transform.position.y);
                if (cameraComponent.orthographic)
                {
                    cameraComponent.orthographicSize += zoomToFieldOfView * orthoZoomSpeed;
                    cameraComponent.orthographicSize = Mathf.Max(cameraComponent.orthographicSize, minZoomLevel);
                }
                else
                {
                    cameraComponent.fieldOfView += zoomToFieldOfView * perspectiveZoomSpeed;
                    cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, minZoomLevel, maxZoomLevel);
                }
            }
            else if (transform.position.y > 11000)
            {
                zoomtoField = false;
            }
        }
        else
        {
            var rotateMultiplier = 1f;
            if (Input.touchCount < 2 && rotating)
            {
                var invert = 1;

                if (Input.touchCount == 1 && !rotatingToStart)
                {
                    var touchZero = Input.GetTouch(0);

                    rotateMultiplier = touchZero.deltaPosition.x * touchRotateMultiplier;
                }
                if (rotatingToStart)
                {
                    rotateMultiplier *= rotatingToStartMultiplier;
                    if (transform.position.x > 0)
                    {
                        invert *= -1;
                    }
                }
                if (rotatingToStart && (transform.rotation.y > 0.981 || transform.rotation.y < -0.981))
                {
                    rotating = false;
                    zoomtoField = true;

                    lerpTarget = new Vector3(0, transform.position.y + 2000, 9665);
                    startTime = Time.time;
                    journeyLength = Vector3.Distance(transform.position, lerpTarget);

                    lerpStarted = true;
                }
                else
                {
                    Debug.Log(transform.rotation.y);

                    transform.RotateAround(point, new Vector3(0.0f, invert * 1.0f, 0.0f), 20f * Time.deltaTime * rotateSpeed * rotateMultiplier);
                }
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
    }
}
