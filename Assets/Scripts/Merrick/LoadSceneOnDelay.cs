using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnDelay : MonoBehaviour {

    [SerializeField] private float delay = 5f;
    [SerializeField] private string sceneName = "";

    private void Start() {
        if (sceneName == "") {
            Debug.LogWarning("no scene name");
            Destroy(this);
        }
        StartCoroutine(load());
    }

    private IEnumerator load() {
        yield return new WaitForSeconds(Mathf.Max(0.2f, delay));
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
