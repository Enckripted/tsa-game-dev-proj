using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuObject;
    public Button ResumeButton;
    public Button QuitButton;
    public string MenuSceneName = "MAIN MENU";
    public bool IsPaused;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.UI.Cancel.performed += ctx => TogglePause();
    }

    private void OnDisable()
    {
        inputActions.UI.Cancel.performed -= ctx => TogglePause();
        inputActions.Disable();
    }

    private void Start()
    {
        if (PauseMenuObject != null)
        {
            PauseMenuObject.SetActive(false);
        }

        if (ResumeButton != null)
        {
            ResumeButton.onClick.AddListener(ResumeGame);
        }

        if (QuitButton != null)
        {
            QuitButton.onClick.AddListener(QuitGame);
        }
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (PauseMenuObject != null) PauseMenuObject.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        if (PauseMenuObject != null) PauseMenuObject.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MenuSceneName);
    }
}
