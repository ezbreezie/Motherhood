using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour {

    public Animator door;
    public AudioSource passSound;

    void OnTriggerEnter(Collider other)
    {
        door.SetTrigger("doorEnter");
        door.ResetTrigger("doorExit");
        passSound.Play();
    }

    void OnTriggerExit(Collider other)
    {
        door.SetTrigger("doorExit");
        door.ResetTrigger("doorEnter");
    }

}
