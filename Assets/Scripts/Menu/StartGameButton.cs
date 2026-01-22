using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StartGameButton : MonoBehaviour
{
    private Button button;

    private void StartGame()
    {
        SceneManager.LoadScene("INTRO SCENE");
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }
}
