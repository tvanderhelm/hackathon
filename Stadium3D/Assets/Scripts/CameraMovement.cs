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
   
    public float lerpSpeed = 50f;
    private Vector3 lerpTarget;
    private float startTime;
    private float journeyLength;
    private bool firstLerpStarted;
    private bool secondLerpStarted;
    public Animator fieldAnimation;



    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
        point = stadium.transform.position;
        transform.LookAt(point);
        fieldAnimation.enabled = false;

    }

    private void Update()
    {
        if (firstLerpStarted)
        {
            if (Lerp())
            {
                firstLerpStarted = false;
                startTime = Time.time;
                lerpTarget = new Vector3(transform.position.x, transform.position.y + 2000, transform.position.z);
                secondLerpStarted = true;
            }
        }
        else if (secondLerpStarted)
        {
            if (this.Lerp())
            {
                secondLerpStarted = false;
                zoomtoField = true;
            }
        }
        else if (zoomtoField)
        {
            ZoomToField();
        }
        else
        {
            this.AutoMovement();
        }
    }

    /// <summary>
    /// Lerp to the target specified in the lerpTarget property.
    /// </summary>
    /// <returns></returns>
    private bool Lerp()
    {
        var distCovered = (Time.time - startTime) * lerpSpeed;
        var fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, lerpTarget, fracJourney);

        return lerpTarget.x - 1.0f <= transform.position.x && lerpTarget.x + 1.0f >= transform.position.x && lerpTarget.y - 1.0f <= transform.position.y && lerpTarget.y + 1.0f >= transform.position.y && lerpTarget.z - 1.0f <= transform.position.z && lerpTarget.z + 1.0f >= transform.position.z;
    }

    /// <summary>
    /// Zoom to the field automatically.
    /// </summary>
    private void ZoomToField()
    {
        transform.RotateAround(point, new Vector3(-1.0f, 0.0f, 0.0f), 20 * Time.deltaTime * rotateSpeed * zoomToFieldMultiplier);
        if (transform.position.y > 8000 && transform.position.y < 11000)
        {
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
            fieldAnimation.enabled = true;
            fieldAnimation.Play("Take 001");
        }
    }

    /// <summary>
    /// Handles the auto movement.
    /// </summary>
    private void AutoMovement()
    {
        if (Input.touchCount < 2 && rotating)
        {
            AutoRotate();
        }
        else if (Input.touchCount == 2)
        {
            ZoomIn();
        }
    }

    /// <summary>
    /// Rotates the camera every second, going faster if the user swipes or wants to go to start.
    /// </summary>
    private void AutoRotate()
    {
        var rotateMultiplier = 1f;
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

            lerpTarget = new Vector3(0, transform.position.y, 9665);
            startTime = Time.time;
            journeyLength = Vector3.Distance(transform.position, lerpTarget);

            firstLerpStarted = true;
        }
        else
        {
            transform.RotateAround(point, new Vector3(0.0f, invert * 1.0f, 0.0f), 20f * Time.deltaTime * rotateSpeed * rotateMultiplier);
        }
    }

    /// <summary>
    /// Handles a pinch and zoom.
    /// </summary>
    private void ZoomIn()
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
