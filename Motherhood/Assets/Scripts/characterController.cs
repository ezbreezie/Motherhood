using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

    public float speed = 10.0F;
    public pausedState paused;
    public GameObject pausedUI;
    public bool mouseHide = true;
    public Camera endCam;
	
	void Update () {

        if (!paused.GetPausedState())
        {

            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);

        }

        if (endCam.enabled == false)
        {

            if (Input.GetKeyDown("escape"))
            {
                if (mouseHide == true)
                {
                    pausedUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    mouseHide = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    pausedUI.SetActive(false);
                    mouseHide = true;
                }
            }
        }

    }
}
