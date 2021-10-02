using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMaker : MonoBehaviour
{

    public Transform point;

    public float speed;
    public float radius;
    public float b;

    [Header("Radius Controller")]
    public float limitRadius = 15f;
    public float speedRadius = 2f;

    [Header("Color Controller")]
    public Color currentColor;
    public Color desireColor;
    public float speedTransition = 2f;
    // Start is called before the first frame update
    void Start()
    {
        currentColor = GetComponentInChildren<TrailRenderer>().startColor;

        StartCoroutine("HelixCurve");
        StartCoroutine("WaitForAnim", 2f);
        StartCoroutine("ColorTransition");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine("AnimRadius");
        }

        if (Input.GetKey(KeyCode.T))
        {
            StopCoroutine("AnimRadius");
        }
    }
    IEnumerator WaitForAnim(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine("AnimRadius");
    }
    IEnumerator HelixCurve()
    {
        float timeS = 0;
        while (true)
        {
            float t = timeS * Mathf.PI / 2;
            point.position = CalculateHelixCurve(t, radius, b);
            yield return new WaitForSeconds(.01f);
            timeS += speed * Time.deltaTime; 
            /*if(timeS > 30f)
            {
                timeS = 0;
            }*/
        }
        
    }

    IEnumerator AnimRadius()
    {
        bool stopIncrease = false;
        while (radius > 0)
        {
            if (radius < limitRadius && stopIncrease == false)
            {
                radius += speedRadius * Time.deltaTime;
                if (radius > limitRadius) stopIncrease = true;
            }
            else
            {
                if (radius < 0.01f)
                {
                    radius = 0;
                }
                else
                {
                    radius -= speedRadius * Time.deltaTime;
                }
                
            }
            yield return new WaitForSeconds(.01f);
        }
        
    }

    IEnumerator ColorTransition()
    {
        float timeT = 0;
        while (point.position.y < 270f)
        {
            GetComponentInChildren<TrailRenderer>().startColor = Color.Lerp(currentColor, desireColor, timeT);
            yield return new WaitForSeconds(.1f);
            timeT += Time.deltaTime / speedTransition;
        }
    }

    Vector3 CalculateHelixCurve(float t, float a, float b)
    {
        Vector3 r = new Vector3(a * Mathf.Cos(t), b * t, a * Mathf.Sin(t));
        return r;
    }

    
}
