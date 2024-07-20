using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject howtoPlayPanel;
    [SerializeField] GameObject mainmenuPanel;

    void Start()
    {
        MainMenuEnable();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void HowToPlayEnable()
    {
        mainmenuPanel.SetActive(false);
        howtoPlayPanel.SetActive(true);
    }

    public void MainMenuEnable()
    {
        mainmenuPanel.SetActive(true);
        howtoPlayPanel.SetActive(false);
    }
}
