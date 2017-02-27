using UnityEngine;
using System.Collections;

public class CreditsCue : MonoBehaviour {

    public Animation fadein;
    public GameObject creditsscreen;
    public Camera endcam;
    private float timer = 0;

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

            if (timer <= -12.0)
            {
                creditsscreen.SetActive(true);
            }

            if (timer <= -10.0)
            {
                momLightLight.SetActive(false);
            }

            if (timer <= -8.0)
            {
                momLight.Play();
                //fillLight.Play();
            }

        }
	
	}
}
