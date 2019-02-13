using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfPlayer : MonoBehaviour {

    [SerializeField] private int player = -1;

    private void Start() {
        if (LivePuzzle.currentPlayer == player) gameObject.SetActive(false);
    }
}
