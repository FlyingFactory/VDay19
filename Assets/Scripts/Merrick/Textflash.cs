using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Textflash : MonoBehaviour {

    private Text text;
    private float timeElapsed = 0;
    [SerializeField] private float flashTime = 0.5f;
    [SerializeField] private float displayTime = 5f;

    private void Start() {
        text = GetComponent<Text>();
        if (text == null) {
            Debug.LogWarning("no text");
            Destroy(this);
        }
    }

    private void Update() {
        timeElapsed += Time.deltaTime;
        if (timeElapsed < flashTime) {
            text.color = new Color(0, 0, 0, timeElapsed / flashTime);
        }
        else if (timeElapsed < flashTime + displayTime) {
            text.color = new Color(0, 0, 0, 1);
        }
        else if (timeElapsed < 2 * flashTime + displayTime) {
            text.color = new Color(0, 0, 0, (2 * flashTime + displayTime - timeElapsed) / flashTime);
        }
        else {
            Destroy(gameObject);
        }
    }
}
