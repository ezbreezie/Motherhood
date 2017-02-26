using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class clueFind : MonoBehaviour {


    [System.Serializable]
    public class clueData
    {
        public Clue clue;
        public GameObject closeupIMG;
        public Button uiButton;
        public GameObject model;
    }

    Clue clue;
    public List<clueData> clues;
    public List<clueData> sclues;

    GameObject mainCamera;
    public Button CLF;
    public Button BRF;
    public Button KIF;
    public Button LRF;
    public Button OFF;
    public GameObject notesUI;
    public GameObject cluesHide;
    public GameObject lockUI;
    public InputField q1;
    private bool isShowing;
    private GameObject activeImage;

    void Start () {

        mainCamera = GameObject.FindWithTag("MainCamera");
        isShowing = false;

    }
	
	void Update () {

        pickup();

    }

    void pickup()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // check if lockbox
                Lockbox lo = hit.collider.GetComponent<Lockbox>();
                //if is lockbox
                if (lo != null)
                {
                    if (lockUI.activeSelf)
                    {
                        lockUI.SetActive(false);
                    }
                    else
                    {
                        lockUI.SetActive(true);
                        q1.ActivateInputField();
                    }
                }

                // check if voicemail
                Voicemail vo = hit.collider.GetComponent<Voicemail>();
                //if is voicemail
                if (vo != null)
                {
                    vo.GetComponent<AudioSource>().Play();
                }

                // check if main clue
                clue = hit.collider.GetComponent<Clue>();

                if (isShowing == false)
                {
                    clueData clueMatch = null;
                    for (int i = 0; i < clues.Count; i++)
                    {
                        if (clues[i].clue == clue)
                        {
                            clueMatch = clues[i];
                            break;
                        }
                    }

                    //this is for when we find a main clue
                    if (clueMatch != null)
                    {
                        clueMatch.uiButton.interactable = true;
                        clueMatch.model.SetActive(false);
                        notesUI.SetActive(true);
                        clueMatch.closeupIMG.SetActive(true);
                        activeImage = clueMatch.closeupIMG;
                        isShowing = true;
                    }
                    //check for subclue
                    else
                    {
                        for (int i = 0; i < sclues.Count; i++)
                        {
                            if (sclues[i].clue == clue)
                            {
                                clueMatch = sclues[i];
                                break;
                            }
                        }

                        if (clueMatch != null)
                        {
                            if (isShowing == false)
                            {
                                notesUI.SetActive(true);
                                clueMatch.closeupIMG.SetActive(true);
                                activeImage = clueMatch.closeupIMG;
                                isShowing = true;
                            }
                            else
                            {
                                notesUI.SetActive(false);
                                activeImage.SetActive(false);
                                isShowing = false;
                            }
                        }
                    }
                }
                else
                {
                    notesUI.SetActive(false);
                    activeImage.SetActive(false);
                    isShowing = false;
                }
            }
        }
    }
}
