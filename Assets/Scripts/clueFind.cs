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

        //bools
    private bool startGame = false;
    private bool hasBear = false;
    private bool bearIn = false;
    private bool hasBlanket = false;
    private bool blanketIn = false;
    private bool hasBook = false;
    private bool bookIn = false;
    private bool hasPicture = false;
    private bool pictureIn = false;
        //objects
    public GameObject boxBear;
    public GameObject pickupBear;
    public GameObject boxStory;
    public GameObject pickupstory;
    public GameObject boxBlanket;
    public GameObject pickupBlanket;
    public GameObject boxPicture;
    public GameObject pickupPicture;

    //end pieces
    public GameObject lightsout;
    public GameObject puzzleEnd;
    public Animation puzzleEndAnim;
    public GameObject endTrigger;
    public GameObject boxofClues;
    public GameObject boxClosed;
        //lullaby fadein
    public AudioSource lullaby;
    private float timeIn;
    public float sp = 1f;
    private bool fadingIn = false;

    //shader hover
    private Shader standard;
    private Shader outline;
    public Renderer nrend;
    public Renderer vrend;
    public Renderer srend;
    public Renderer brend;

    public GameObject startNote;
    public GameObject notesUI;
    public GameObject puzzleUI;
    public GameObject controls;
    public MeshCollider openDoor;
    public MeshCollider brDoor;
    public MeshCollider cDoor;
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
        //Lullaby Setup
        lullaby.spatialBlend = sp;
        //Find Shaders
        standard = Shader.Find("Standard");
        outline = Shader.Find("Outlined/Silhouetted Bumped");
    }
	
	void Update () {

        //Lullaby Setup
        lullaby.spatialBlend = sp;
        timeIn = Time.deltaTime;

        if (fadingIn == true)
        {
            sp -= 0.01f * timeIn;

            if (sp <= 0f)
            {
                sp = 0f;
                timeIn = 0f;
            }
        }

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
            // check if finished letter
            EndLetter el = hit.collider.GetComponent<EndLetter>();

            //End Letter
            if (el != null)
            {
                nrend.material.shader = outline;

                if (Input.GetMouseButtonDown(0))
                {
                    if (puzzleShowing)
                    {
                        puzzleEnd.SetActive(false);
                        puzzleShowing = false;
                        return;
                    }
                    else
                    {
                        puzzleEnd.SetActive(true);
                        puzzleShowing = true;
                        return;
                    }
                }

            } else
            {
                nrend.material.shader = standard;
            }

            //Submit clue to box (if have main blue)
            if (box != null && hasBear || box != null && hasBlanket || box != null && hasBook || box != null && hasPicture)
            {
                brend.material.shader = outline;

                if (Input.GetMouseButtonDown(0))
                {
                    if (hasBear)
                    {
                        bearIn = true;
                        hasBear = false;
                        boxBear.SetActive(true);
                        pickupBear.SetActive(false);
                    }

                    if (hasBlanket)
                    {
                        blanketIn = true;
                        hasBlanket = false;
                        boxBlanket.SetActive(true);
                        pickupBlanket.SetActive(false);
                    }

                    if (hasBook)
                    {
                        bookIn = true;
                        hasBook = false;
                        boxStory.SetActive(true);
                        pickupstory.SetActive(false);
                    }

                    if (hasPicture)
                    {
                        pictureIn = true;
                        hasPicture = false;
                        boxPicture.SetActive(true);
                        pickupPicture.SetActive(false);
                    }
                }
            } else
            {
                brend.material.shader = standard;
            }

            //startGame interaction
            if (sg != null)
            {
                srend.material.shader = outline;

                if (Input.GetMouseButtonDown(0))
                {
                    startGame = true;
                    startNote.SetActive(false);
                    puzzleUI.SetActive(true);
                    puzzleShowing = true;
                    return;
                }
            }
            else
            {
                srend.material.shader = standard;
            }

            //Disable hover if raycast not touching anything import
            if (vo != null || clue != null || box != null)
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

            //if gameStart paper has been activated
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

                //Deactivate hover and interact if has main clue
                if (!hasBear && !hasBlanket && !hasBook && !hasPicture)
                {
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
                                puzzleUI.SetActive(true);
                                clueMatch.clearSentence.SetActive(true);
                                clueMatch.blurSentence.GetComponent<Animation>().enabled = true;
                                puzzleShowing = true;

                                if (clueMatch.isBear)
                                {
                                    hasBear = true;
                                    return;
                                }

                                if (clueMatch.isBlanket)
                                {
                                    hasBlanket = true;
                                    return;
                                }

                                if (clueMatch.isBook)
                                {
                                    hasBook = true;
                                    return;
                                }

                                if (clueMatch.isPicture)
                                {
                                    hasPicture = true;
                                    return;
                                }
                            }

                            //check for subclue
                            if (sclueMatch != null && touching == true)
                            {
                                notesUI.SetActive(true);
                                sclueMatch.closeupIMG.SetActive(true);
                                activeImage = sclueMatch.closeupIMG;
                                isShowing = true;
                                return;
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
        }
        else
        {
            touching = false;
        }

        //If puzzleUI showing (Maybe display button change?)
        if (puzzleShowing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                puzzleUI.SetActive(false);
                puzzleEnd.SetActive(false);
                puzzleShowing = false;
                controls.SetActive(true);
            }
            if (Input.GetMouseButtonDown(1))
            {
                puzzleUI.SetActive(false);
                puzzleEnd.SetActive(false);
                puzzleShowing = false;
            }
        } else
        {
            if (startGame)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    puzzleUI.SetActive(true);
                    puzzleShowing = true;
                }
            }
        }

        //First Clue Found: Game Start
        if (hasBear == true)
        {
            openDoor.enabled = false;
        }

        //End Puzzle
        if (bearIn && blanketIn && bookIn && pictureIn)
        {
            endGame();
            puzzleEnd.SetActive(true);
            puzzleEndAnim.enabled = true;
        }

        if (puzzleEndAnim.enabled == true)
        {
            if (puzzleEndAnim.isPlaying == false)
            {
                puzzleShowing = true;
                puzzleEndAnim.enabled = false;
                boxofClues.SetActive(false);
                boxClosed.SetActive(true);
            }
        }

    }

    void endGame()
    {
        bearIn = false;
        blanketIn = false;
        pictureIn = false;
        bookIn = false;
        startGame = false;
        lightsout.SetActive(false);
        endTrigger.SetActive(true);
        openDoor.enabled = true;
        brDoor.enabled = true;
        cDoor.enabled = true;
        fadingIn = true;
    }

}
