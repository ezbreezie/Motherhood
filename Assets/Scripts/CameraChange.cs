using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour {

    public Camera startcam;
    public Camera playcam;
    public Camera endcam;

    public Button start;
    public GameObject startscreen;
    public GameObject endscreen;
    public GameObject crosshair;

	void Start () {

        //Disable starting camera

        startcam.enabled = false;
        endcam.enabled = false;
        //playcam.enabled = false;

        Button btn = start.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }
	
	void Update () {

        if (startcam.enabled == true)
        {
            startscreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;

        } else
        {
            startscreen.SetActive(false);
        }

        if (playcam.enabled == true)
        {
            crosshair.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;

        } else if (endscreen.activeSelf)
        {
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;

        } else if (endcam.enabled == true)
        {
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

        } else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    void TaskOnClick(){

        Cursor.lockState = CursorLockMode.Locked;

    }

}
