using System.Linq;
using UnityEngine;

public class UpgradeStadium : MonoBehaviour
{

	public GameObject stadiumSmall;
	public GameObject stadiumHuge;

	public void OnClick()
	{
		var anim = stadiumSmall.GetComponent<Animation>();
		anim.Play("stadium_disappear");
	}

	public void AnimComplete(int phase)
	{
		stadiumSmall.SetActive(false);
		stadiumHuge.SetActive(true);
		var anim = stadiumHuge.GetComponent<Animation>();
		anim.Play("stadium_appear");
	}
}
