using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject neonHeart;
    [SerializeField] private float neonHeartDelay = 1.0f;
    private float activationTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    [SerializeField] private List<GameObject> successSetActive = new List<GameObject>(1);

    // Update is called once per frame
    void Update() {

        activationTime += Time.deltaTime;

        if (activationTime > neonHeartDelay)
        {
            neonHeart.transform.SetParent(this.transform);
            neonHeart.transform.localPosition = new Vector3(0, 1.0f, 0);
            neonHeart.SetActive(true);
        }

        if (activationTime >= 5.0f) {
            foreach (GameObject g in successSetActive) g.SetActive(true);
            Destroy(this);
        }
    }
}
