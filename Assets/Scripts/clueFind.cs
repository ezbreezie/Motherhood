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
        public bool isPillow;
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
    private bool hasPillow = false;
    private bool PillowIn = false;
    private bool hasBook = false;
    private bool bookIn = false;
    private bool hasPicture = false;
    private bool pictureIn = false;
        //objects
    public GameObject boxBear;
    public GameObject pickupBear;
    public GameObject boxStory;
    public GameObject pickupstory;
    public GameObject boxPillow;
    public GameObject pickupPillow;
    public GameObject boxPicture;
    public GameObject pickupPicture;

    //end pieces
    public GameObject lightsout;
    public GameObject puzzleEnd;
    public Animation puzzleEndAnim;
    public GameObject endTrigger;
    public GameObject boxofClues;
    public GameObject boxClosed;
    public GameObject safeGuard;
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
    public Renderer trend;
    public Renderer torend;
    public Renderer torend2;
    public Renderer drend;
    public Color red;
    public Color yellow;

    public GameObject startNote;
    public GameObject notesUI;
    public GameObject puzzleUI;
    public GameObject controls;
    public MeshCollider openDoor;
    public MeshCollider brDoor;
    public MeshCollider cDoor;
    public AudioSource paper;
    public AudioSource ducky;
    private bool isShowing;
    private bool puzzleShowing;
    private GameObject activeImage;
    private Renderer activeHoverM;
    private Renderer activeHoverS;

    //interactivity
    public GameObject tvOn;
    private bool tvIsOn = false;
    public AudioSource flush;

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
            // check if tv
            TV tv = hit.collider.GetComponent<TV>();
            // check if toilet
            Toilet t = hit.collider.GetComponent<Toilet>();
            //check if duck
            Ducky d = hit.collider.GetComponent<Ducky>();

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
                        paper.Play();
                        return;
                    }
                }

            } else
            {
                nrend.material.shader = standard;
            }

            //Submit clue to box (if have main clue)
            if (box != null && hasBear || box != null && hasPillow || box != null && hasBook || box != null && hasPicture)
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

                    if (hasPillow)
                    {
                        PillowIn = true;
                        hasPillow = false;
                        boxPillow.SetActive(true);
                        pickupPillow.SetActive(false);
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
                    paper.Play();
                    return;
                }
            }
            else
            {
                srend.material.shader = standard;
            }

            //Disable hover if raycast not touching anything import
            if (vo != null || clue != null || box != null || tv != null)
            {
                touching = true;
            }
            else
            {
                touching = false;
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
                
                //VO Hover
                if (touching == true && vo != null)
                {
                    vrend.material.shader = outline;
                }
                else
                {
                    vrend.material.shader = standard;
                }

                //If is Ducky
                if (d != null)
                {
                    drend.material.shader = outline;

                    if (Input.GetMouseButtonDown(0))
                    {
                        ducky.Play();
                    }

                }
                else
                {
                    drend.material.shader = standard;
                }

                //If is Toilet
                if (t != null)
                {
                    torend.material.shader = outline;
                    torend2.material.shader = outline;

                    if (Input.GetMouseButtonDown(0))
                    {
                        flush.Play();
                    }

                }
                else
                {
                    torend.material.shader = standard;
                    torend2.material.shader = standard;
                }

                //If is TV
                if (tv != null)
                {
                    trend.material.shader = outline;

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (tvIsOn)
                        {
                            tvOn.SetActive(false);
                            tvIsOn = false;
                            return;
                        }
                        else
                        {
                            tvOn.SetActive(true);
                            tvIsOn = true;
                            return;
                        }
                    }

                }
                else
                {
                    trend.material.shader = standard;
                }

                //OnHover Clue
                if (clueMatch != null)
                {
                    if (!hasBear && !hasPillow && !hasBook && !hasPicture)
                    {
                        activeHoverM = clueMatch.crend;
                        activeHoverM.material.shader = outline;
                        activeHoverM.material.SetColor("_OutlineColor", yellow);
                    } else
                    {
                        activeHoverM = clueMatch.crend;
                        activeHoverM.material.shader = outline;
                        activeHoverM.material.SetColor("_OutlineColor", red);
                    }

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
                            if (!hasBear && !hasPillow && !hasBook && !hasPicture)
                            {
                                clueMatch.model.SetActive(false);
                                clueMatch.inHandmodel.SetActive(true);
                                puzzleUI.SetActive(true);
                                clueMatch.clearSentence.SetActive(true);
                                clueMatch.blurSentence.GetComponent<Animation>().enabled = true;
                                puzzleShowing = true;
                                paper.Play();

                                if (clueMatch.isBear)
                                {
                                    hasBear = true;
                                    return;
                                }

                                if (clueMatch.isPillow)
                                {
                                    hasPillow = true;
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
                            } else
                            {
                                // do nothing/play sound
                            }

                        }

                        //check for subclue
                        if (sclueMatch != null && touching == true)
                        {
                            notesUI.SetActive(true);
                            sclueMatch.closeupIMG.SetActive(true);
                            activeImage = sclueMatch.closeupIMG;
                            isShowing = true;
                            paper.Play();
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
                    paper.Play();
                }
            }
        }

        //First Clue Found: Game Start
        if (hasBear == true)
        {
            openDoor.enabled = false;
        }

        //End Puzzle
        if (bearIn && PillowIn && bookIn && pictureIn)
        {
            endGame();
            puzzleEnd.SetActive(true);
            paper.Play();
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
        PillowIn = false;
        pictureIn = false;
        bookIn = false;
        startGame = false;
        lightsout.SetActive(false);
        endTrigger.SetActive(true);
        safeGuard.SetActive(true);
        openDoor.enabled = true;
        brDoor.enabled = true;
        cDoor.enabled = true;
        fadingIn = true;
    }

}
