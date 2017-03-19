using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class clueFind : MonoBehaviour {


    [System.Serializable]
    public class clueData
    {
        public Clue clue;
        public Renderer crend;
        public GameObject model;
        public GameObject closeupIMG;
        public GameObject inHandmodel;
        public GameObject clearSentence;
        public GameObject blurSentence;
        public bool isBear;
        public bool isBlanket;
        public bool isBook;
        public bool isPicture;
    }

    Clue clue;
    public List<clueData> clues;
    public List<clueData> sclues;

    //puzzle
    private bool startGame = false;
    private bool hasBear = false;
    private bool bearIn = false;
    private bool hasBlanket = false;
    private bool blanketIn = false;
    private bool hasBook = false;
    private bool bookIn = false;
    private bool hasPicture = false;
    private bool pictureIn = false;
    public GameObject boxBear;
    public GameObject pickupBear;
    public GameObject endTrigger;

    private Shader standard;
    private Shader outline;
    public Renderer vrend;
    public Renderer srend;

    public GameObject startNote;
    public GameObject notesUI;
    public GameObject puzzleUI;
    public MeshCollider openDoor;
    private bool isShowing;
    private bool puzzleShowing;
    private GameObject activeImage;
    private Renderer activeHoverM;
    private Renderer activeHoverS;

    //raycast
    public float distance;
    RaycastHit hit;
    private bool touching = false;

    void Start () {
        //ClueUI
        isShowing = false;
        //PuzzleUI
        puzzleShowing = false;
        //Find Shaders
        standard = Shader.Find("Standard");
        outline = Shader.Find("Outlined/Silhouetted Bumped");
    }
	
	void Update () {

        /*if (puzzleUI.activeSelf)
        {
            Debug.Log("ok...");
        } else
        {
            Debug.Log("no...");
        }
        Debug.Log(puzzleShowing); */
        Debug.Log(hasBear);
        Debug.Log(touching);
        Debug.DrawRay(this.transform.position, this.transform.forward * distance, Color.blue);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, distance))
        {
            // check if voicemail
            Voicemail vo = hit.collider.GetComponent<Voicemail>();
            // check if clue
            clue = hit.collider.GetComponent<Clue>();
            // check if beginning
            startGame sg = hit.collider.GetComponent<startGame>();
            // check if box
            Box box = hit.collider.GetComponent<Box>();

            if (box != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("ok..");
                    if (hasBear == true)
                    {
                        bearIn = true;
                        boxBear.SetActive(true);
                        pickupBear.SetActive(false);
                    } 
                } 
            }

            if (sg != null)
            {
                srend.material.shader = outline;

                if (Input.GetMouseButtonDown(0))
                {
                    startGame = true;
                    startNote.SetActive(false);
                    // doesn't work after this ??
                    puzzleUI.SetActive(true);
                    puzzleShowing = true;
                }
            }
            else
            {
                srend.material.shader = standard;
            }

            if (vo != null || clue != null)
            {
                touching = true;
            }
            else
            {
                touching = false;
            }

            //OnHover VO
            if (touching == true && vo != null)
            {
                vrend.material.shader = outline;
            }
            else
            {
                vrend.material.shader = standard;
            }

            if (startGame)
            {
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

                /* if (hasBear || hasBlanket || hasBook || hasPicture)
                {
                    clueMatch.model.layer = 2;
                    sclueMatch.model.layer = 2;
                    vo.gameObject.layer = 2;
                }
                else
                {
                    clueMatch.model.layer = 0;
                    sclueMatch.model.layer = 2;
                    vo.gameObject.layer = 0;
                } */

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
                        if (clueMatch != null && touching == true)
                        {
                            clueMatch.model.SetActive(false);
                            clueMatch.inHandmodel.SetActive(true);
                            puzzleShowing = true;

                            if (clueMatch.isBear)
                            {
                                hasBear = true;
                            }

                            if (clueMatch.isBlanket)
                            {
                                hasBear = true;
                            }

                            if (clueMatch.isBear)
                            {
                                hasBear = true;
                            }

                            if (clueMatch.isBear)
                            {
                                hasBear = true;
                            }
                        }
                        //check for subclue
                        if (sclueMatch != null && touching == true)
                        {
                            notesUI.SetActive(true);
                            sclueMatch.closeupIMG.SetActive(true);
                            activeImage = sclueMatch.closeupIMG;
                            isShowing = true;
                        }
                    }
                    else
                    {
                        if (activeImage != null)
                        {
                            activeImage.SetActive(false);
                            notesUI.SetActive(false);
                        }

                        isShowing = false;
                    }
                }
            }
        }
        else
        {
            touching = false;
        }

        if (puzzleShowing == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                puzzleUI.SetActive(false);
                puzzleShowing = false;
            }
        }

        if (hasBear == true)
        {
            openDoor.enabled = false;
        }

        //End Puzzle
        if (bearIn)
        {
            endTrigger.SetActive(true);
        }
    }
}
