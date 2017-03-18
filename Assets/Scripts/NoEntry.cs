using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEntry : MonoBehaviour {

    public MeshCollider door;
    public BoxCollider enterTrig;

    void Update()
    {
        if (door.enabled == true)
        {
            enterTrig.enabled = false;
        } else
        {
            enterTrig.enabled = true;
        }
    }

}
