using UnityEngine;
public class FieldButton : MonoBehaviour
{
    public void OnClick()
    {
        var cameraScript = Camera.main.gameObject.GetComponent<CameraMovement>();

       cameraScript.rotatingToStart = true;
    }
}
