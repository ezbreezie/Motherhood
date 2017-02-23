using UnityEngine;
using System.Collections;

public class RayCastHitLight : MonoBehaviour {

    GameObject mainCamera;

    public GameObject lightBR;

    void Start () {

        mainCamera = GameObject.FindWithTag("MainCamera");

    }
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Lightswitch li = hit.collider.GetComponent<Lightswitch>();

                if (li != null)
                {
                    if (lightBR.activeSelf)
                    {
                        lightBR.SetActive(false);
                    }
                    else
                    {
                        lightBR.SetActive(true);
                    }
                }
            }
        }
    }
}
