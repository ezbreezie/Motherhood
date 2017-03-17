using UnityEngine;
using System.Collections;

public class toggleClues : MonoBehaviour {

    public GameObject clues;
    private bool isShowing;
    public bool mouseHide = true;

    void Update () {

        if (Input.GetKeyDown("c"))
        {
            isShowing = !isShowing;
            clues.SetActive(isShowing);

        }

    }

}
