using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassBehaviour : MonoBehaviour
{

    private const float INTERVAL = 1.0f;
    private const float SWAY_ANGLE = 0.4f;
    private float startTime;
    private enum SwayDirection { left, right };
    private SwayDirection GrassDirection;

    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(-8.0f, 0, 0);
        startTime = Time.time;
        GrassDirection = SwayDirection.left;
    }

    // Update is called once per frame
    void Update()
    {
        float t = (Time.time - startTime)/INTERVAL;
        if (t >= 1)
        {
            if (GrassDirection == SwayDirection.left) GrassDirection = SwayDirection.right;
            else if (GrassDirection == SwayDirection.right) GrassDirection = SwayDirection.left;
            startTime = Time.time;
        } 

        if (GrassDirection == SwayDirection.left)
        {
            transform.Rotate(Vector3.left * Mathf.SmoothStep(-SWAY_ANGLE, -SWAY_ANGLE, t));
        }
        else if (GrassDirection == SwayDirection.right)
        {
            transform.Rotate(Vector3.right * Mathf.SmoothStep(-SWAY_ANGLE, -SWAY_ANGLE, t));

        }
    }
}
