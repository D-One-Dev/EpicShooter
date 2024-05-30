using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }
}
