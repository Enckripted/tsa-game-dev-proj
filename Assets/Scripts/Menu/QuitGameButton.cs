using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class QuitGameButton : MonoBehaviour
{
    private Button button;

    private void QuitGame()
    {
        Application.Quit();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(QuitGame);
    }
}
