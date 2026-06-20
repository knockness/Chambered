using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{

    [Header("Animation Components")]
    public Animator playerAnimator;
    public int animationFrameRate;

    [Header("Currently Active Coroutine")]
    public Coroutine currentlyActiveCoroutine;

    [Header("States")]
    public PlayerStateScript.PlayerState previouseState;

    [Header ("Script Access")]
    public PlayerStateScript playerStateScript;

    void Awake()
    {
        playerStateScript = GetComponentInParent<PlayerStateScript>();
        playerAnimator = GetComponent<Animator>();

        previouseState = playerStateScript.state;
        currentlyActiveCoroutine = null;

        animationFrameRate = 24;
    }

    void Update()
    {

//This is to make sure the switch statement only runs when the state actually switches, preventing animations for replaying over and over.
        if(previouseState == playerStateScript.state)
            return;
            
        if(currentlyActiveCoroutine != null)
        {
            StopCoroutine(currentlyActiveCoroutine);
            currentlyActiveCoroutine = null;
        }

        previouseState = playerStateScript.state;

        switch (playerStateScript.state)
        {
            case PlayerStateScript.PlayerState.Idle:
                break;

            case PlayerStateScript.PlayerState.Walk:
                break;

            case PlayerStateScript.PlayerState.Dash:
                break;
        }
    }

//Animation Coroutines 
//The nurmerator in the wait time is how long the entry animations are in frames.
}
