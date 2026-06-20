using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header ("Directional Variables")]
    public float horizontalInput;
    public float verticalInput;

    [Header ("Vectors")]
    public Vector2 magnitude;
    public Vector2 rawInput;

    [Header ("Speed Variables")]
    public float speed;
    public float dashSpeed;

    [Header ("Player Instances")]
    public Rigidbody2D playerRigidbody;

    [Header ("Switch State Permission Booleans")]
    public bool canDash;

    [Header ("Storage Variables")]
    public int horizontalStorage;
    public int verticalStorage;
    public float speedStorage;

    [Header ("Time Variables")]
    public int dashFrames;
    public int dashCooldownFrames;

    [Header ("Script Access")]
    public PlayerStateScript playerStateScript;

    [Header("Input Actions")]
    public InputAction playerMovementControls;
    public InputAction playerDashControls;

    [Header("Animation Components")]
    public Animator playerAnimator;

    void OnEnable()
    {
        playerMovementControls.Enable();
        playerDashControls.Enable();
    }

    void OnDisable()
    {
        playerMovementControls.Disable();
        playerDashControls.Disable();
    }

    void Awake()
    {
        playerStateScript = GetComponent<PlayerStateScript>();
        playerAnimator = playerStateScript.playerAnimationScript.playerAnimator;

        speedStorage = speed;
        canDash = true;
    }

    void FixedUpdate()
    {
        switch (playerStateScript.state)
        {
            case PlayerStateScript.PlayerState.Dash:
                speed = 0;
                if (horizontalInput == 0 && verticalInput == 0)
                {
                    playerRigidbody.linearVelocity = new Vector2(horizontalStorage, verticalStorage) * dashSpeed;
                }
                else
                {
                    playerRigidbody.linearVelocity = magnitude * dashSpeed;
                }
                break;
            
            case PlayerStateScript.PlayerState.Idle:
            case PlayerStateScript.PlayerState.Walk:
                speed = speedStorage;
                playerRigidbody.linearVelocity = speed * magnitude;
                break;
        }
    }

    void Update()
    {
        if (!playerStateScript.isBusy)
        {
            rawInput = playerMovementControls.ReadValue<Vector2>();
            magnitude = rawInput.magnitude > 1 ? rawInput.normalized : rawInput;

            horizontalInput = rawInput.x;
            verticalInput = rawInput.y;

            if (verticalInput > 0f)
            {
                verticalStorage = 1; //The player faces forwards.
                horizontalStorage = 0;
            }
            else if (verticalInput < 0f)
            {
                verticalStorage = -1; //The player faces backwards.
                horizontalStorage = 0;
            }
            else if (horizontalInput > 0f)
            {
                horizontalStorage = 1; //The player faces right.
                verticalStorage = 0;
            }
            else if (horizontalInput < 0f)
            {
                horizontalStorage = -1; //The player faces left.
                verticalStorage = 0;
            }

            if (canDash
             && !playerStateScript.isBusy
             && playerDashControls.triggered)
            {
                playerStateScript.state = PlayerStateScript.PlayerState.Dash;
                StartCoroutine(DashTime(dashFrames));
                speed = 0;
            }

            if(playerStateScript.state != PlayerStateScript.PlayerState.Dash)
            {
                if(rawInput == Vector2.zero)
                {
                    playerAnimator.SetFloat("storageX", horizontalStorage);
                    playerAnimator.SetFloat("storageY", verticalStorage);
                    playerStateScript.state = PlayerStateScript.PlayerState.Idle;
                } else
                {
                    playerAnimator.SetFloat("inputX", rawInput.x);
                    playerAnimator.SetFloat("inputY", rawInput.y);
                    playerStateScript.state = PlayerStateScript.PlayerState.Walk;
                }
            }

        }
    }

    public IEnumerator DashTime(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
            yield return new WaitForFixedUpdate();

        playerRigidbody.linearVelocity = Vector2.zero;
        playerStateScript.state = PlayerStateScript.PlayerState.Idle;
        StartCoroutine(DashCooldown(dashCooldownFrames));
        yield break;
    }

    public IEnumerator DashCooldown(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            canDash = false;
            yield return new WaitForFixedUpdate();
        }

        canDash = true;
        yield break;
    }
}