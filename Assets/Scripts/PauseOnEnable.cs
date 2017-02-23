using UnityEngine;
using System.Collections;

public class PauseOnEnable : MonoBehaviour {

    public pausedState pauser;

    void OnEnable()
    {
        pauser.SetPausedState(true);
    }

    void OnDisable()
    {
        pauser.SetPausedState(false);
    }

}
