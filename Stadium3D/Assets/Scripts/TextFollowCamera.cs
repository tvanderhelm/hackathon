using UnityEngine;

public class TextFollowCamera : MonoBehaviour
{
    void Update()
    {
        var lookPosition = Camera.main.transform.position - transform.position;
        lookPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }
}
