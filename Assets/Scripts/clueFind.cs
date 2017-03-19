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
        public Renderer crend;
    }

    Clue clue;
    public List<clueData> clues;
    public List<clueData> sclues;

    private Shader standard;
    private Shader outline;
    public Renderer vrend;

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
    private Renderer activeHoverM;
    private Renderer activeHoverS;

    //raycast
    public float distance;
    RaycastHit hit;
    private bool touching = false;
    public GameObject otherObject;

    void Start () {
        
        isShowing = false;

        standard = Shader.Find("Standard");
        outline = Shader.Find("Outlined/Silhouetted Bumped");

    }
	
	void Update () {
 
        Debug.DrawRay(this.transform.position, this.transform.forward * distance, Color.blue);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance))
        {
            touching = true;

            // check if voicemail
            Voicemail vo = hit.collider.GetComponent<Voicemail>();
            // check if clue
            clue = hit.collider.GetComponent<Clue>();

            if (vo != null || clue != null)
            {
                touching = true;
            } else
            {
                touching = false;
            }

            //OnHover VO
            if (touching == true && vo != null)
            {
                vrend.material.shader = outline;
            } else {
                vrend.material.shader = standard;
            }

            clueData clueMatch = null;
            clueData sclueMatch = null;

            for (int i = 0; i < clues.Count; i++)
            {
                if (clues[i].clue == clue)
                {
                    clueMatch = clues[i];
                    break;
                }
            }

            for (int s = 0; s < sclues.Count; s++)
            {
                if (sclues[s].clue == clue)
                {
                    sclueMatch = sclues[s];
                    break;
                }
            }

            //OnHover Clue
            if (clueMatch != null)
            {
                activeHoverM = clueMatch.crend;
                activeHoverM.material.shader = outline;

            }
            else if (sclueMatch != null)
            {
                activeHoverS = sclueMatch.crend;
                activeHoverS.material.shader = outline;
            }
            else
            {
                if (activeHoverM != null)
                {
                    activeHoverM.material.shader = standard;
                }

                if (activeHoverS != null)
                {
                    activeHoverS.material.shader = standard;
                }
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
                    //main clue
                    if (clueMatch != null)
                    {
                        clueMatch.model.SetActive(false);
                        notesUI.SetActive(true);
                        clueMatch.closeupIMG.SetActive(true);
                        activeImage = clueMatch.closeupIMG;
                        isShowing = true;
                    }
                    //check for subclue
                    if (sclueMatch != null)
                    {
                        notesUI.SetActive(true);
                        sclueMatch.closeupIMG.SetActive(true);
                        activeImage = sclueMatch.closeupIMG;
                        isShowing = true;
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
