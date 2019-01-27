using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGenerator : MonoBehaviour
{
    private LineRenderer line;
    private List<GameObject> rope;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        rope = DrawRope(line);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private GameObject NewCylinder(Vector3 start, Vector3 end)
    {
        const float CYLINDER_RADIUS = 0.1f;
        const float CYLINDER_LENGTH = 0.57f;
        
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.transform.SetParent(this.GetComponentInParent<Transform>());
        cylinder.transform.localPosition = Vector3.Lerp(start, end, 0.5f);
        cylinder.transform.localRotation = Quaternion.FromToRotation(Vector3.up, end - start);
        cylinder.transform.localScale = new Vector3(CYLINDER_RADIUS, (end - start).magnitude*CYLINDER_LENGTH, CYLINDER_RADIUS);
        cylinder.GetComponent<MeshRenderer>().material = Resources.Load("Rope") as Material;
        return cylinder;
    }

    private List<GameObject> DrawRope(LineRenderer line)
    {
        List<GameObject> rope = new List<GameObject>();
        for (int i = 0; i < line.positionCount - 1; i++)
        {
            rope.Add(NewCylinder(line.GetPosition(i), line.GetPosition(i + 1)));
        }

        return rope;
    }
}
