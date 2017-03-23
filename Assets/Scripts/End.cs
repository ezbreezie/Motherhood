using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

    public Camera playcam;
    public GameObject endScene;
    public Camera endCam;
    public GameObject fadeInEnd;
    public Animation fadeInAnim;
    public GameObject fadeout;
    public AudioSource passerby;

    void Update() {

        if (fadeInEnd.activeSelf && fadeInAnim.isPlaying == false)
        {
            endScene.SetActive(true);
            endCam.enabled = true;
            playcam.enabled = false;
            fadeInEnd.SetActive(false);
            fadeout.SetActive(true);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        fadeInEnd.SetActive(true);
        passerby.Play();

    }

}
