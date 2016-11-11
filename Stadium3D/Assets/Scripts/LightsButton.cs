using System;
using UnityEngine;

public class LightsButton : MonoBehaviour
{
    public Light Sun;
    public Light UpperRightLight;
    public Light LowerRightLight;
    public Light UpperLeftLight;
    public Light LowerLeftLight;

    private void Start()
    {
        if (DateTime.Now.Hour > 14 || DateTime.Now.Hour < 7)
        {
            Sun.enabled = false;
            UpperRightLight.enabled = true;
            LowerRightLight.enabled = true;
            UpperLeftLight.enabled = true;
            LowerLeftLight.enabled = true;

            ToggleHalo(UpperRightLight, true);
            ToggleHalo(LowerRightLight, true);
            ToggleHalo(UpperLeftLight, true);
            ToggleHalo(LowerLeftLight, true);
        }
    }

    /// <summary>
    /// Toggle lights.
    /// </summary>
    public void OnClick()
    {
        Sun.enabled = !Sun.enabled;
        UpperRightLight.enabled = !UpperRightLight.enabled;
        LowerRightLight.enabled = !LowerRightLight.enabled;
        UpperLeftLight.enabled = !UpperLeftLight.enabled;
        LowerLeftLight.enabled = !LowerLeftLight.enabled;

        ToggleHalo(UpperRightLight);
        ToggleHalo(LowerRightLight);
        ToggleHalo(UpperLeftLight);
        ToggleHalo(LowerLeftLight);
    }

    /// <summary>
    /// Toggle halo of one specific light. Can force the halo to be enabled.
    /// </summary>
    private void ToggleHalo(Light lightSource, bool forceEnable = false)
    {
        var halo = (Behaviour) lightSource.GetComponent("Halo");

        if (forceEnable)
        {
            halo.enabled = true;
        }
        else
        {
            halo.enabled = !halo.enabled;
        }
    }
}
