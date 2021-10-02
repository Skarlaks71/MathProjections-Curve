using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public Transform point;
    public float height;
    public Color nightColor;
    public float speedTransition = 0.05f;

    float dstToPoint;
    Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        dstToPoint  = (point.position - transform.position).magnitude;
        currentColor = GetComponent<Camera>().backgroundColor;
        StartCoroutine("NightTransition");
    }

    // Update is called once per frame
    void Update()
    {
        if(point.position.y < 283f)
            transform.position = new Vector3(transform.position.x, point.position.y + dstToPoint - height, transform.position.z);

        

    }
    IEnumerator NightTransition()
    {
        float timeT = 0;
        while (point.position.y < 270f)
        {
            GetComponent<Camera>().backgroundColor = Color.Lerp(currentColor,nightColor,  timeT);
            yield return new WaitForSeconds(.1f);
            timeT += Time.deltaTime / speedTransition;
        }
    }
}
