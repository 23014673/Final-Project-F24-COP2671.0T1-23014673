using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePause : MonoBehaviour
{
    public TextMeshProUGUI pauseButtonText;
    public Button pauseButton;

    void Start()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
    }

    public void TogglePause()
    {
        if (!GameManager.isGameActive) return;

        if (GameManager.isPaused)
        {
            Time.timeScale = 1f;

            pauseButtonText.text = "Pause";

            GameManager.isPaused = false;
        }
        else
        {
            Time.timeScale = 0f;

            pauseButtonText.text = "Resume";

            GameManager.isPaused = true;
        }
    }
}