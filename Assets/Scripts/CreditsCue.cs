using UnityEngine;
using System.Collections;

public class CreditsCue : MonoBehaviour {

    public Animation fadein;
    public GameObject creditsscreen;
    public Camera endcam;
    private float timer = 0;
    public Animation MomBB;
    public AudioSource Dad;
    private bool dadPlay;

    //End Scene Lights Trigger
    public Animation momLight;
    public Animation fillLight;
    public GameObject momLightLight;
    public GameObject fillLightLight;

    void Start () {
	
	}
	
	void Update () {

        if (endcam.enabled == true) {

            timer -= Time.deltaTime;

            if (timer <= -13.0)
            {
                creditsscreen.SetActive(true);
            }

            if (timer <= -11.0)
            {
                momLightLight.SetActive(false);
            }

            if (timer <= -9.0)
            {
                momLight.Play();
            }

            if (timer <= -6.0)
            {
                dadPlay = true;
            }

            if (timer <= -1)
            {
               MomBB.Play();
            }

        }

        if (dadPlay == false)
        {
            Dad.Play();
        }
	
	}
}
