using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DebuggingToolsScript : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    public HealthScript playerHealthScript;
    public InputAction debugControls;

    void OnEnable()
    {
        debugControls.Enable();
    }

    void OnDisable()
    {
        debugControls.Disable();
    }

    void Update()
    {
        playerHealthText.text = playerHealthScript.health.ToString();

        if (debugControls.triggered)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
