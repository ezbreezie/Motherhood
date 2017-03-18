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

    private Shader standard;
    private Shader outline;
    public Renderer vrend;
    private Renderer crend;

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

    //raycast
    public float distance;
    RaycastHit hit;
    private bool touching = false;

    void Start () {
        
        isShowing = false;

        standard = Shader.Find("Standard");
        outline = Shader.Find("Outlined/Diffuse");

        crend = gameObject.GetComponent<Renderer>();

    }
	
	void Update () {
        Debug.Log(gameObject);
        Debug.DrawRay(this.transform.position, this.transform.forward * distance, Color.blue);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance))
        {
            touching = true;
            // check if voicemail
            Voicemail vo = hit.collider.GetComponent<Voicemail>();
            // check if main clue
            clue = hit.collider.GetComponent<Clue>();

            if (vo != null || clue != null)
            {
                touching = true;
            } else
            {
                touching = false;
            }

            //OnHover
            if (touching == true && vo != null)
            {
                vrend.material.shader = outline;
            } else {
                vrend.material.shader = standard;
            }

            if (touching == true && clue != null)
            {
                crend.material.shader = outline;

            }
            else
            {
                crend.material.shader = standard;
            }

            //OnClick
            if (Input.GetMouseButtonDown(0))
            {
                //if is voicemail
                if (vo != null)
                {
                    vo.GetComponent<AudioSource>().Play();
                }

                //if is clue
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

        } else
        {
            touching = false;
        }

    }
    
}
