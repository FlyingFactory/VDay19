using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlacementStarMap : MonoBehaviour {

    private bool detect1 = false;
    [SerializeField] private GameObject trigger1 = null;
    private bool detect2 = false;
    [SerializeField] private GameObject trigger2 = null;

    [SerializeField] private GameObject successDisplay = null;
    private float currentDisplayTime = 0;
    [SerializeField] private float displayTime = 5f;
    private bool startedDisplay = false;
    private bool endedDisplay = false;


    private float currentDetectionTime = 0;
    [SerializeField] private float detectionTime = 2f;
    
    [SerializeField] private float bufferMult = 1.5f;

    private void Start() {
        if (trigger1 == null || trigger2 == null || successDisplay == null) {
            Debug.LogWarning("Missing reference in DetectPlacement, deleting");
            Destroy(this);
        }
    }

    private void Update() {
        if (!startedDisplay) {
            if (detect1 && detect2) {
                currentDetectionTime += Time.deltaTime;
                if (currentDetectionTime > detectionTime) {
                    startedDisplay = true;
                    successDisplay.SetActive(true);
                }
            }
            else {
                currentDetectionTime = Mathf.Max(0, currentDetectionTime - Time.deltaTime * bufferMult);
            }
        }
        else if (!endedDisplay) {
            currentDisplayTime += Time.deltaTime;
            if (currentDisplayTime > displayTime) {
                endedDisplay = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetInstanceID() == trigger1.GetInstanceID()) {
            detect1 = true;
        }
        else if (other.gameObject.GetInstanceID() == trigger2.GetInstanceID()) {
            detect2 = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetInstanceID() == trigger1.GetInstanceID()) {
            detect1 = false;
        }
        else if (other.gameObject.GetInstanceID() == trigger2.GetInstanceID()) {
            detect2 = false;
        }
    }
}
