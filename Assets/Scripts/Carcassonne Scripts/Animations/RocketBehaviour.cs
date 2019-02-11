using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject neonHeart;
    [SerializeField] private float neonHeartDelay = 1.0f;
    private float activationTime;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
        activationTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (activationTime > neonHeartDelay)
        {
            neonHeart.transform.SetParent(this.transform);
            neonHeart.transform.localPosition = new Vector3(0, 1.0f, 0);
            neonHeart.SetActive(true);
        }
    }
}
