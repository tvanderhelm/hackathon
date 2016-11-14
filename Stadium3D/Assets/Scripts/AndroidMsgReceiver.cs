using UnityEngine;
using System.Collections;

public class AndroidMsgReceiver : MonoBehaviour
{
    
    private bool zoomed;

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

                LightsButton lightsButton = GameObject.Find("AndroidMsgReceiver").GetComponent<LightsButton>();
                lightsButton.OnClick();

                break;
            case "field":
                FieldButton fieldButton = GameObject.Find("AndroidMsgReceiver").GetComponent<FieldButton>();
                fieldButton.OnClick();
                break;
            default:
                Debug.Log("Uncaught string: " + s);
                break;
        }
    }
}
