using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlacementGeneral : MonoBehaviour {

    private List<bool> detections = new List<bool>();
    [SerializeField] private List<GameObject> triggerColliders = new List<GameObject>(2);
    [SerializeField] private List<GameObject> successSetActive = new List<GameObject>(1);

    private float currentDetectionTime = 0;
    [SerializeField] private float detectionTime = 2f; // after this time of continuous detection, it is a success.
    [SerializeField] private float bufferMult = 1.5f; //time where it is NOT detected will reduce the current detection time by this multiplier, instead of imeediately resetting.

    private void Start() {
        if (triggerColliders == null || successSetActive == null) {
            Debug.LogWarning("Missing reference in DetectPlacement, deleting");
            Destroy(this);
        }
        triggerColliders.RemoveAll(i => i == null);
        successSetActive.RemoveAll(i => i == null);
        if (triggerColliders.Count == 0 || successSetActive.Count == 0) {
            Debug.LogWarning("Empty list in DetectPlacement, deleting");
            Destroy(this);
        }
        for (int i = 0; i < triggerColliders.Count; i++) {
            detections.Add(false);
        }
    }

    private void Update() {
        bool allActive = true;
        foreach (bool b in detections) {
            allActive &= b;
            Debug.Log(allActive);
        }
        if (allActive) {
            currentDetectionTime += Time.deltaTime;
            if (currentDetectionTime > detectionTime) {
                foreach (GameObject g in successSetActive) g.SetActive(true);
                Destroy(this);
            }
        }
        else {
            currentDetectionTime = Mathf.Max(0, currentDetectionTime - Time.deltaTime * bufferMult);
        }
    }

    private void OnTriggerEnter(Collider other) {
        for (int i = 0; i < triggerColliders.Count; i++) {
            if (other.gameObject.GetInstanceID() == triggerColliders[i].gameObject.GetInstanceID()) {
                detections[i] = true;
                break;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        for (int i = 0; i < triggerColliders.Count; i++) {
            if (other.gameObject.GetInstanceID() == triggerColliders[i].gameObject.GetInstanceID()) {
                detections[i] = false;
                break;
            }
        }
    }
}
