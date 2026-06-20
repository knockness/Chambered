using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebuggingToolsScript : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    public HealthScript playerHealthScript;

    void Update()
    {
        playerHealthText.text = playerHealthScript.health.ToString();

        if ((Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.LeftControl)) && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
