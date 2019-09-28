using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Salir : MonoBehaviour {
    private float time;
    void Start() {
        time = 0f;
    }

    void Update() {
        time = time + 1f;
        if (time >= 500f) SceneManager.LoadScene(0);
    }
}