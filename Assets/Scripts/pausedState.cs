using UnityEngine;
using System.Collections;

public class pausedState : MonoBehaviour {

    bool paused = false;

    public void SetPausedState(bool state)
    {
        paused = state;
    } 

    public bool GetPausedState()
    {
        return paused;
    } 

}
