using System.Linq;
using UnityEngine;

public class UpgradeStadium : MonoBehaviour
{

	public GameObject stadiumSmall;
	public GameObject stadiumHuge;

	public void OnClick()
	{
	    if (stadiumSmall.activeSelf)
	    {
	        var anim = stadiumSmall.GetComponent<Animation>();
	        anim.Play("stadium_disappear");
	    } else {
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
	    } else {
            stadiumHuge.SetActive(false);
            stadiumSmall.SetActive(true);
            var anim = stadiumSmall.GetComponent<Animation>();
            anim.Play("stadium_appear");
	    }
		
	}
}
