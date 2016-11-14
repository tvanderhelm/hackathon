﻿using UnityEngine;
using System.Collections;

public class MsgReceiver : MonoBehaviour
{

    public void ReceiveMsg(string s)
    {
        Debug.Log("receive msg");

        switch (s)
        {
            case "stadium":
                var smallStadium = GameObject.Find("stadium_small") ?? GameObject.Find("stadium_huge");
                smallStadium.GetComponent<UpgradeStadium>().OnClick();
                break;
            case "lights":

                break;
            case "field":

                break;
            default:
                Debug.Log("Uncaught string: " + s);
                break;
        }
    }
}
