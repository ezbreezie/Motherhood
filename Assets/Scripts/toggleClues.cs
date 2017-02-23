using UnityEngine;
using System.Collections;

public class toggleClues : MonoBehaviour {

    public GameObject clues;
    private bool isShowing;

    void Update () {

        if (Input.GetKeyDown("c"))
        {
            isShowing = !isShowing;
            clues.SetActive(isShowing);

            if (clues.activeSelf) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

    }

}
