using UnityEngine;
using System.Collections;

public class FieldButton : MonoBehaviour
{

    private Vector3 point;
    public GameObject stadium;
    public float rotateSpeed;

    // Use this for initialization
    void Start()
    {
        point = stadium.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        var camera = Camera.main;
        var cameraScript = camera.gameObject.GetComponent<CameraMovement>();

        cameraScript.rotatingToStart = true;
    }
}
