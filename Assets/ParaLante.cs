using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaLante : MonoBehaviour {
    public float speed = 10f;
    public float time = 2f;
    public float verSpeed = 20f;
    public float moment;
    public float direction = 1f;
	// Use this for initialization
	void Start () {
        moment = Time.time; 
	}
	
	// Update is called once per frame
	void Update () {
            transform.position += Vector3.right * speed * Time.deltaTime;
            Vector3 vertical = Vector3.up * verSpeed * Time.deltaTime * direction;
            if (Time.time > moment + time)
            {
                direction *= -1f;
                moment = Time.time;
            }
            transform.position += vertical;
	}
}
