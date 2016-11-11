using UnityEngine;

public class FieldButton : MonoBehaviour
{
    public void OnClick()
    {
        var mainCamera = Camera.main;
        var cameraScript = mainCamera.gameObject.GetComponent<CameraMovement>();
        
        if (cameraScript.rotating && cameraScript.rotatingToStart == false)
        {
            cameraScript.rotatingToStart = true;

            if (mainCamera.transform.position.x > 0)
            {
                cameraScript.shouldInvert = true;
            }
        }
    }
}
