using System;
using UnityEngine;

public class LightsButton : MonoBehaviour
{
    public Light Sun;
    public Light UpperRightLight;
    public Light LowerRightLight;
    public Light UpperLeftLight;
    public Light LowerLeftLight;

    private bool showHalo = true;

    private void Start()
    {
        if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 7)
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

        if (showHalo)
        {
            ToggleHalo(UpperRightLight);
            ToggleHalo(LowerRightLight);
            ToggleHalo(UpperLeftLight);
            ToggleHalo(LowerLeftLight);
        }
    }

    /// <summary>
    /// Toggle halo of one specific light. Can force the halo to be enabled or disabled.
    /// </summary>
    private void ToggleHalo(Light lightSource, bool force = false, bool forceState = true)
    {
        var halo = (Behaviour) lightSource.GetComponent("Halo");

        if (force)
        {
            halo.enabled = forceState;
        }
        else
        {
            halo.enabled = !halo.enabled;
        }
    }

    /// <summary>
    /// Turn the halo's on or off if the stadium is upgraded.
    /// </summary>
    /// <param name="small"></param>
    public void SwitchStadium(bool small)
    {
        if (small)
        {
            // Enable the halo's.
            showHalo = true;

            if (!Sun.enabled)
            {
                ToggleHalo(UpperRightLight, true);
                ToggleHalo(LowerRightLight, true);
                ToggleHalo(UpperLeftLight, true);
                ToggleHalo(LowerLeftLight, true);
            }
            
        }
        else
        {
            // Turn all halo's off.
            ToggleHalo(UpperRightLight, true, false);
            ToggleHalo(LowerRightLight, true, false);
            ToggleHalo(UpperLeftLight, true, false);
            ToggleHalo(LowerLeftLight, true, false);

            showHalo = false;
        }
    }
}
