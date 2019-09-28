using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giro : MonoBehaviour
{
    void Start() {

    }

    void Update()
    {
        transform.Rotate(Vector3.back, 300f * Time.deltaTime);
    }
}