using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool rotating = true;
    public bool rotatingToStart;
    public float perspectiveZoomSpeed = 0.5f;
    public float orthoZoomSpeed = 0.5f;
    public float rotateSpeed = 7f;
    public float minZoomLevel = 15f;
    public float maxZoomLevel = 80f;
    public float minAutoZoomLevel = 30f;
    public float originalZoomLevel = 55f;
    public float touchRotateMultiplier = 2f;
    public float zoomToFieldOfView = -0.5f;
    public float rotatingToStartMultiplier = 0.02f;
    public float turningRate = 30f;
    public float lerpSpeed = 50f;

    public bool shouldInvert;

    public bool zoomToField;
    public bool zoomFromField;
    private bool zoomed;

    public LineRenderer Line;
    public MeshRenderer StadiumName;
    public MeshRenderer StadiumLocation;
    public FieldButton fieldButton;
    private Vector3 point;

    public GameObject stadium;
    public Camera cameraComponent;

    public GameObject field;

    private Vector3 lerpTarget;
    private float startTime;
    private float journeyLength;
    private bool firstLerpStarted;


    private readonly Quaternion originalRotatingTarget = new Quaternion(0f, 0.98f, -0.2f, 0f);
    private readonly Quaternion zoomedInRotatingTarget = new Quaternion(0f, 0.831f, -0.556f, 0f);

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();
        point = stadium.transform.position;
        transform.LookAt(point);
    }

    private void Update()
    {
        if (firstLerpStarted)
        {
            if (Lerp())
            {
                firstLerpStarted = false;
                startTime = Time.time;
                lerpTarget = new Vector3(0, 110, 24);
                journeyLength = Vector3.Distance(transform.position, lerpTarget);
                zoomToField = true;
                ToggleStadiumName(false);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotatingTarget, turningRate * Time.deltaTime);
            }
        }
        else if (zoomToField)
        {
            ZoomToField();
        }
        else if (zoomFromField)
        {
            ZoomFromField();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            startTime = Time.time;
            transform.LookAt(stadium.transform.position);
            lerpTarget = new Vector3(0, 62, 223);
            journeyLength = Vector3.Distance(transform.position, lerpTarget);
            zoomFromField = true;

            var anim = field.GetComponent<Animation>();
            anim["Take 001"].speed = -1.0f;
            if (!anim.isPlaying)
            {
                anim["Take 001"].time = anim["Take 001"].clip.length;

            }
            anim.Play("Take 001");
            fieldButton.ChangeButtonState(false);
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
        var distCovered = (Time.time - startTime) * lerpSpeed/100;
        var fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Slerp(transform.position, lerpTarget, fracJourney);

        return lerpTarget.x - 1.0f <= transform.position.x && lerpTarget.x + 1.0f >= transform.position.x && lerpTarget.y - 1.0f <= transform.position.y && lerpTarget.y + 1.0f >= transform.position.y && lerpTarget.z - 1.0f <= transform.position.z && lerpTarget.z + 1.0f >= transform.position.z;
    }

    /// <summary>
    /// Zoom to the field automatically.
    /// </summary>
    private void ZoomToField()
    {
        if (Lerp())
        {
            zoomToField = false;
            var anim = field.GetComponent<Animation>();
            anim["Take 001"].speed = 1.0f;
            anim.Play("Take 001");
            fieldButton.ChangeButtonState(true);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, zoomedInRotatingTarget, turningRate * Time.deltaTime);
        }

        if (cameraComponent.orthographic)
        {
            cameraComponent.orthographicSize += zoomToFieldOfView * orthoZoomSpeed;
            cameraComponent.orthographicSize = Mathf.Max(cameraComponent.orthographicSize, minAutoZoomLevel);
        }
        else
        {
            cameraComponent.fieldOfView += zoomToFieldOfView * perspectiveZoomSpeed;
            cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, minAutoZoomLevel, maxZoomLevel);
        }
    }

    /// <summary>
    /// Zoom from the field to the starting position automatically.
    /// </summary>
    private void ZoomFromField()
    {
        if (Lerp())
        {
            zoomFromField = false;
            zoomed = false;
            rotating = true;
            ToggleStadiumName(true);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotatingTarget, turningRate * Time.deltaTime);
        }

        if (cameraComponent.orthographic)
        {
            cameraComponent.orthographicSize -= zoomToFieldOfView * orthoZoomSpeed;
            cameraComponent.orthographicSize = Mathf.Max(cameraComponent.orthographicSize, minZoomLevel);
        }
        else
        {
            cameraComponent.fieldOfView -= zoomToFieldOfView * perspectiveZoomSpeed;
            cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, minAutoZoomLevel, originalZoomLevel);
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
        var rotateMultiplier = 5f;
        var invert = 1;

        if (Input.touchCount == 1 && !rotatingToStart)
        {
            var touchZero = Input.GetTouch(0);

            rotateMultiplier = touchZero.deltaPosition.x * touchRotateMultiplier;
        }
        if (rotatingToStart)
        {
            rotateMultiplier *= rotatingToStartMultiplier;
            if (shouldInvert)
            {
                invert *= -1;
            }
        }
        if (rotatingToStart && transform.position.z > 222)
        {
            rotating = false;
            rotatingToStart = false;

            lerpTarget = new Vector3(0, transform.position.y, 223);
            startTime = Time.time;
            journeyLength = Vector3.Distance(transform.position, lerpTarget);

            firstLerpStarted = true;
        }
        else
        {
            //Debug.Log(transform.rotation.y);
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

    public void ToggleStadiumName(bool enabled)
    {
        Line.enabled = enabled;
        StadiumName.enabled = enabled;
        StadiumLocation.enabled = enabled;
    }
}
