using UnityEngine;

public class LightsButton : MonoBehaviour
{
    public Light Sun;
    public Light UpperRightLight;
    public Light LowerRightLight;
    public Light UpperLeftLight;
    public Light LowerLeftLight;

    private GameObject g;

    public void OnClick()
    {
        Sun.enabled = !Sun.enabled;
        UpperRightLight.enabled = !UpperRightLight.enabled;
        LowerRightLight.enabled = !LowerRightLight.enabled;
        UpperLeftLight.enabled = !UpperLeftLight.enabled;
        LowerLeftLight.enabled = !LowerLeftLight.enabled;

        DisableHalo(UpperRightLight);
        DisableHalo(LowerRightLight);
        DisableHalo(UpperLeftLight);
        DisableHalo(LowerLeftLight);
    }

    private void DisableHalo(Light lightSource)
    {
        var halo = (Behaviour) lightSource.GetComponent("Halo");
        halo.enabled = !halo.enabled;
    }
}
