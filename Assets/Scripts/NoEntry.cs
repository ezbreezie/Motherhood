using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEntry : MonoBehaviour {

    public GameObject NotEntry;

    void OnTriggerEnter(Collider other)
    {
        NotEntry.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        NotEntry.SetActive(false);
    }

}
