using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject objectiveCanvas;
    [SerializeField] private GameObject pauseMenu;

    [Header("verificacoes")]
    [SerializeField] private bool isPaused;

    private void Awake()
    {
        objectiveCanvas = GameObject.FindGameObjectWithTag("objective");
        pauseMenu = GameObject.FindGameObjectWithTag("pause");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            HandlePause();
        }
    }

    public void HandlePause()
    {
        if (!isPaused)
        {
            SetPauseMenuChildren(true);
            SetObjectiveMenuChildren(false);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            SetPauseMenuChildren(false);
            SetObjectiveMenuChildren(true);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetPauseMenuChildren(bool active)
    {
        foreach (Transform child in pauseMenu.transform)
        {
            child.gameObject.SetActive(active);
        }
    }

    private void SetObjectiveMenuChildren(bool active)
    {
        foreach (Transform child in objectiveCanvas.transform)
        {
            child.gameObject.SetActive(active);
        }
    }
}
