using System;
using NUnit.Framework;
using UnityEngine;

public class PlayerStateScript : MonoBehaviour
{
    [Header ("Script Access")]
    public PlayerMovementScript playerMovementScript;
    public HealthScript playerHealthScript;
    public PlayerAnimationScript playerAnimationScript;

    [Header ("Switch State Permission Booleans")]
    public bool debugIsBusy; //This is just here because isBusy will not show in the inspector due to the getter.
    public bool isBusy => state == PlayerState.Dash; 
           //            || state == PlayerState.State

    [Header ("Public Player State")]
    public PlayerState state;

    void Awake()
    {
        playerMovementScript = GetComponent<PlayerMovementScript>();
        playerHealthScript = GetComponent<HealthScript>();
        playerAnimationScript = GetComponentInChildren<PlayerAnimationScript>();
    }

    void Update()
    {
        debugIsBusy = isBusy;
    }

    public enum PlayerState
    {
        Idle,
        Walk,
        Dash,
    }
}
