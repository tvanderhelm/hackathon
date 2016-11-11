using UnityEngine;

public class FieldButton : MonoBehaviour
{
    public TextureChanger textureChanger; 
    private bool zoomed = false;
    public void OnClick()
    {
        var mainCamera = Camera.main;
        var cameraScript = mainCamera.gameObject.GetComponent<CameraMovement>();

        if (!zoomed && cameraScript.rotating && cameraScript.rotatingToStart == false)
        {
            cameraScript.rotatingToStart = true;

            if (mainCamera.transform.position.x > 0)
            {
                cameraScript.shouldInvert = true;
            }
        }
        else {
            // Change the background textures
           // var textureChanger = gameObject.GetComponent<TextureChanger>();
            textureChanger.StartUpgrade();
        }
    }

    public void changeButtonState(bool zoomed) {
        this.zoomed = zoomed;
    }
 
}
