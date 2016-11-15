using System;
using UnityEngine;

public class LightsButton : MonoBehaviour
{
    public Light Sun;
    public Light UpperRightLight;
    public Light LowerRightLight;
    public Light UpperLeftLight;
    public Light LowerLeftLight;

    public Material NightSkybox;
    public Material DaySkybox;

    private readonly Color _dayFogColor = new Color(0.612f, 0.69f, 0.337f, 1f);
    private readonly Color _nightFogColor = new Color(0.255f, 0.35f, 0.224f, 1f);

    private void Start()
    {
        if (DateTime.Now.Hour > 18 || DateTime.Now.Hour < 7)
            SetDayNight(true);
    }

    /// <summary>
    /// Toggle day/night
    /// </summary>
    public void OnClick()
    {
        SetDayNight(true);
    }

    public void SetDayNight(bool toggle = false)
    {
        if (toggle)
            Sun.enabled = !Sun.enabled;

        SetBigLights();
        SetSmallLights();
        SetSkybox();

        DynamicGI.UpdateEnvironment();
    }

    private void SetSkybox()
    {
        if (Sun.enabled)
        {
            RenderSettings.skybox = DaySkybox;
            RenderSettings.fogColor = _dayFogColor;
        }
        else
        {
            RenderSettings.skybox = NightSkybox;
            RenderSettings.fogColor = _nightFogColor;
        }
    }

    private void SetSmallLights()
    {
        if (GameObject.Find("stadium_small") != null)
        {
            var onOff = !Sun.enabled;

            UpperRightLight.enabled = onOff;
            LowerRightLight.enabled = onOff;
            UpperLeftLight.enabled = onOff;
            LowerLeftLight.enabled = onOff;

            SetHalo(UpperRightLight);
            SetHalo(LowerRightLight);
            SetHalo(UpperLeftLight);
            SetHalo(LowerLeftLight);
        }
    }

    private void SetHalo(Light lightSource)
    {
        var halo = (Behaviour)lightSource.GetComponent("Halo");
        halo.enabled = !Sun.enabled;
    }

    private void SetBigLights()
    {
        var hugeStadium = GameObject.Find("stadium_huge");
        if (hugeStadium != null)
        {
            foreach (Transform child in hugeStadium.transform)
            {
                if (child.GetComponent("Light") != null)
                {
                    ((Light)child.GetComponent("Light")).enabled = !Sun.enabled;
                }
                if (child.GetComponent("Halo") != null)
                {
                    ((Behaviour)child.GetComponent("Halo")).enabled = !Sun.enabled;
                }
            }
        }
    }
}
