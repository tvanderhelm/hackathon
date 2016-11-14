using UnityEngine;

public class UpgradeStadium : MonoBehaviour
{
	public GameObject stadiumSmall;
	public GameObject stadiumHuge;
    private LightsButton lightsScript;

    private void Start()
    {
        lightsScript = GameObject.Find("Lights Button").GetComponent<LightsButton>();
    }

	public void OnClick()
	{
	    if (stadiumSmall.activeSelf)
        {
            lightsScript.SwitchStadium(false);
	        var anim = stadiumSmall.GetComponent<Animation>();
	        anim.Play("stadium_disappear");
	    }
        else
        {
            var anim = stadiumHuge.GetComponent<Animation>();
            anim.Play("stadium_disappear");
        }
	}

	public void AnimComplete(int phase)
	{
	    if (stadiumSmall.activeSelf)
	    {
            stadiumSmall.SetActive(false);
            stadiumHuge.SetActive(true);
            var anim = stadiumHuge.GetComponent<Animation>();
            anim.Play("stadium_appear");
	    }
        else
        {
            stadiumHuge.SetActive(false);
            stadiumSmall.SetActive(true);
            var anim = stadiumSmall.GetComponent<Animation>();
            anim.Play("stadium_appear");
	    }
	}

    public void AppearAnimationComplete(int phase)
    {
        if (stadiumSmall.activeSelf)
        {
            lightsScript.SwitchStadium(true);
        }
    }
}
