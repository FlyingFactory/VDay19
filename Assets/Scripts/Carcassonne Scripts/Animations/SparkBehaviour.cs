using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject rocket;

    private static List<GameObject> ropeGO;
    private static Vector3[] ropePos;

    private readonly float SPEED_FACTOR = 0.5f;
    private int k;
    private int j;
    private float startTime;
    private bool reinitialise;

    // Start is called before the first frame update
    void Start()
    {
        ropeGO = LivePuzzle.ropeGO;
        startTime = Time.time;
        k = -1;
        reinitialise = true;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateSpark();
    }

    private void AnimateSpark()
    {
        if (reinitialise)
        {
            k++;
            if (k >= ropeGO.Count)
            {
                // Set off fireworks function
                rocket.transform.SetParent(ropeGO[k-1].transform);
                rocket.transform.localPosition = new Vector3(0, 0, 0);
                rocket.SetActive(true);
                Destroy(this.gameObject);
            }
            else
            {
                ropePos = new Vector3[ropeGO[k].GetComponentInChildren<LineRenderer>().positionCount];
                ropeGO[k].GetComponentInChildren<LineRenderer>().GetPositions(ropePos);
                reinitialise = false;
                j = 0;
                gameObject.transform.SetParent(ropeGO[k].GetComponentInChildren<LineRenderer>().gameObject.transform);
            }
        }
        else
        {
            float distance = Vector3.Distance(ropePos[j], ropePos[j + 1]);
            float totalTime = distance / SPEED_FACTOR;
            float t = (Time.time - startTime) / totalTime;
            this.transform.localPosition = Vector3.Lerp(ropePos[j], ropePos[j + 1], t) + new Vector3(0, 0.02f, 0);

            if (t >= 1)
            {
                startTime = Time.time;
                j++;

                if (j >= ropePos.Length - 1)
                {
                    reinitialise = true;
                }
            }
        }
    }
}
