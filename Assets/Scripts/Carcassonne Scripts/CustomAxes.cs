using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomAxes
{
    public static Vector3 customXUnitVector;
    public static Vector3 customYUnitVector;
    public static Vector3 customZUnitVector;

    public static void InitialiseVectors(GameObject origin)
    {
        customXUnitVector = new Vector3(1, 0, 0);
        customXUnitVector.z -= Mathf.Tan(origin.transform.eulerAngles.y);
        customXUnitVector.y += Mathf.Tan(origin.transform.eulerAngles.z);
        customXUnitVector = customXUnitVector.normalized;

        customYUnitVector = new Vector3(0, 1, 0);
        customYUnitVector.z += Mathf.Tan(origin.transform.eulerAngles.x);
        customYUnitVector.x -= Mathf.Tan(origin.transform.eulerAngles.z);
        customYUnitVector = customYUnitVector.normalized;

        customZUnitVector = new Vector3(0, 0, 1);
        customZUnitVector.x += Mathf.Tan(origin.transform.eulerAngles.y);
        customZUnitVector.y -= Mathf.Tan(origin.transform.eulerAngles.x);
        customZUnitVector = customZUnitVector.normalized;
    }
}
