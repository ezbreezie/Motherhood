using UnityEngine;
using System.Collections;

public class End : MonoBehaviour {

    public Camera playcam;
    public Camera endcam;
    public Animation fadein;
    public Animation end;
    public GameObject fadeinobj;

    void OnTriggerEnter(Collider other)
    {

        playcam.enabled = false;
        endcam.enabled = true;
        fadein.Play();
        end.Play();

    }

}
