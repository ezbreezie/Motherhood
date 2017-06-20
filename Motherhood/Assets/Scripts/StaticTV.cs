using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTV : MonoBehaviour {

 private float scrollSpeed = 0.5f;
 private Renderer rend;
 private float offset;
 
 
void Start()
    {
        rend = GetComponent<Renderer>();
    }


void Update()
    {
        offset = Random.Range(0, Time.time * scrollSpeed);
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
