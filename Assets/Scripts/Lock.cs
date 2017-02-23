using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lock : MonoBehaviour {

    public InputField q1;
    public InputField q2;
    public InputField q3;
    public InputField q4;
    public InputField q5;
    public GameObject wrongText;
    public GameObject rightText;
    bool q1a;
    bool q2a;
    bool q3a;
    bool q4a;
    bool q5a;
    public GameObject endtrigger;

    public void Start () {
        
        q1a = false;
        q2a = false;
        q3a = false;
        q4a = false;
        q5a = false;

    }
	
	void Update () {

        if (q1.text == "" || q2.text == "" || q3.text == "" || q4.text == "" || q5.text == "")
        {
            //do nothing (wait for full)
        } else
        {
            Check();
        }

        if (q1a == true && q2a == true && q3a == true && q4a == true && q5a == true)
        {
            Right();
        }

	}

        void Check() {

        if (q1.text == "S" || q1.text == "s")
            {
                q1a = true;
            } else
            {
                Wrong();
        }

        if (q2.text == "O" || q2.text == "o")
            {
                q2a = true;
            }
            else
            {
                Wrong();
        }

        if (q3.text == "R" || q3.text == "r")
            {
                q3a = true;
            }
            else
            {
                Wrong();
        }

        if (q4.text == "R" || q4.text == "r")
            {
                q4a = true;
            }
            else
            {
                Wrong();
        }

        if (q5.text == "Y" || q5.text == "y")
            {
                q5a = true;
            }
            else
            {
                Wrong();
        }
       
    }

    void Wrong()
    {
        q1.text = "";
        q2.text = "";
        q3.text = "";
        q4.text = "";
        q5.text = "";
        q1.ActivateInputField();
        wrongText.SetActive(true);
    }

    void Right()
    {
        wrongText.SetActive(false);
        rightText.SetActive(true);
        endtrigger.SetActive(true);
    }

}
