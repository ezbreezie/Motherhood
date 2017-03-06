using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

    public Camera playcam;
    public Camera endcam;
    public Animation fadein;
    public GameObject fadeinobj;
    public GameObject doorTrigger;
    public GameObject endTrigger;

    void Update() {
        if (endTrigger.activeSelf) {
            doorTrigger.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        playcam.enabled = false;
        endcam.enabled = true;
        fadein.Play();

    }

}
