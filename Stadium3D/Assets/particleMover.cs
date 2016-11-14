using UnityEngine;
using System.Collections;

public class particleMover : MonoBehaviour {
    Vector3 startPoint, endPoint;
    public bool moving;
    // Use this for initialization
    void Start () {
        startPoint = transform.position;

    }
    public IEnumerator TranslateTo(Transform thisTransform, Vector3 endPos, float value)
    {
        yield return Translation(thisTransform, thisTransform.position, endPos, value);
    }

    public IEnumerator Translation(Transform thisTransform, Vector3 endPos, float value)
    {
        yield return Translation(thisTransform, thisTransform.position, thisTransform.position + endPos, value);
    }

    private IEnumerator Translation(Transform thisTransform, Vector3 startPos, Vector3 endPos, float value)
    {
        float rate =  1.0f / value;
        float t = 0.0f;
        while (t < 1.0)
        {
            t += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            yield return null;
        }
    }
    

    // Update is called once per frame
    void Update () {
        if (moving)
        {
            StartCoroutine(Translation(gameObject.transform, Vector3.left, 0.5f));
            moving = false;
        }
    }
}
