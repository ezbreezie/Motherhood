using UnityEngine;
using System.Collections;

public class characterController : MonoBehaviour {

    public float speed = 10.0F;
    public pausedState paused;

    void Start (){

        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update () {

        if (!paused.GetPausedState())
        {

            float translation = Input.GetAxis("Vertical") * speed;
            float straffe = Input.GetAxis("Horizontal") * speed;
            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0, translation);

            if (Input.GetKeyDown("escape"))
                Cursor.lockState = CursorLockMode.None;
        }
    }
}
