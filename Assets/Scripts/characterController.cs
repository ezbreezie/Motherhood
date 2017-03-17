using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

    public float speed = 10.0F;
    public pausedState paused;
    public GameObject pausedUI;
    public bool mouseHide = true;
	
	void Update () {

        if (!paused.GetPausedState())
        {

            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);

        }

        if (paused.GetPausedState() == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

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
                pausedUI.SetActive(false);
                mouseHide = true;
            }
        }

    }
}
