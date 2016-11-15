using UnityEngine;

public class FieldButton : MonoBehaviour
{
    public TextureChanger textureChanger; 
    private bool zoomed;

    public void OnClick()
    {
        var mainCamera = Camera.main;
        var cameraScript = mainCamera.gameObject.GetComponent<CameraMovement>();

        if (!zoomed && cameraScript.rotating && cameraScript.rotatingToStart == false)
        {
            cameraScript.rotatingToStart = true;

            if (mainCamera.transform.position.x > 0)
                cameraScript.shouldInvert = true;

            textureChanger.initField();
        }
        else
        {
            // Change the background textures
            textureChanger.StartUpgrade();
        }
    }

    public void ChangeButtonState(bool zoomed)
    {
        this.zoomed = zoomed;
    }
}
