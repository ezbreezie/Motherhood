using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour {

    public Camera startcam;
    public Camera playcam;
    public Camera endcam;

    public Button start;
    public GameObject startscreen;
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
        } else
        {
            crosshair.SetActive(false);
        }

    }

    void TaskOnClick(){

        Cursor.lockState = CursorLockMode.Locked;

    }

}
