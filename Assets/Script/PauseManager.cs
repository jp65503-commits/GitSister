using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject objectiveCanvas;
    [SerializeField] private GameObject pauseMenu;

    [Header("verificacoes")]
    [SerializeField] private bool isPaused;
    [SerializeField] private bool isReading;
    [SerializeField] public bool isInCutscene;

    private void Awake()
    {
        objectiveCanvas = GameObject.FindGameObjectWithTag("objective");
        pauseMenu = GameObject.FindGameObjectWithTag("pause");
    }

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Escape) && !isReading && !isInCutscene)
        {
            HandlePause(false);
        }
    }

    public void HandlePause(bool livro)
    {
        if (livro)
        {
            if (isReading)
            {
                return;
            }

            isReading = true;
            isPaused = true;

            SetObjectiveMenuChildren(false);
            Time.timeScale = 0f;
            return;
        }

        if (!isPaused)
        {
            isPaused = true;
            SetPauseMenuChildren(true);
            SetObjectiveMenuChildren(false);
            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;
            SetPauseMenuChildren(false);
            SetObjectiveMenuChildren(true);
            Time.timeScale = 1f;
        }
    }

    public void FecharLivro()
    {
        isReading = false;
        isPaused = false;

        SetObjectiveMenuChildren(true);
        Time.timeScale = 1f;
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