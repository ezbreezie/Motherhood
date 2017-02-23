﻿using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

    GameObject mainCamera;
    bool carrying;
    GameObject carriedObject;
    public float distance;
    public float smooth;

	void Start () {

        mainCamera = GameObject.FindWithTag("MainCamera");
    
	}
	
	void Update () {

        if (carrying) {
            carry(carriedObject);
            checkDrop();
        }else{
            pickup();
        }
	
	}

    void carry(GameObject o){
        o.GetComponent<Rigidbody>().useGravity = false;
        //o.transform.position = Vector3.Lerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
        o.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth));
    }

    void pickup(){
        if (Input.GetMouseButtonDown(0)){
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if(p != null){
                    carrying = true;
                    carriedObject = p.gameObject;
                }
            }
        }
    }

    void checkDrop(){
        if (Input.GetMouseButtonDown(0))
        {
            dropObject();
        }
    }

    void dropObject(){
        carrying = false;
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject = null;
    }

}
