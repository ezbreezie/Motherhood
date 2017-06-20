using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameFall : MonoBehaviour {

    public Animation pic;
    public AudioSource bang;
    public Collider col;

    void OnTriggerEnter(Collider other)
    {
        pic.Play();
        bang.Play();
    }

    void OnTriggerExit(Collider other)
    {
        col.enabled = false;
    }
}
