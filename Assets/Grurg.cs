using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grurg : MonoBehaviour {
    public AudioSource sourceAudio;
    public AudioSource sourceAudio2;
    public AudioSource sourceAudio3;
    public float time = 10f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float ran = Random.Range(0f, 1f);
        if (ran < 0.02 && !sourceAudio.isPlaying) sourceAudio.Play();
        ran = Random.Range(0f, 1f);
        if (ran < 0.02 && !sourceAudio2.isPlaying) sourceAudio2.Play();
        ran = Random.Range(0f, 1f);
        if (ran < 0.02 && !sourceAudio3.isPlaying) sourceAudio3.Play();
        if (Time.time >= time)
        {
            Application.Quit();
        }
    }
}
