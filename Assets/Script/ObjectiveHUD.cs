using System.Collections;
using TMPro;
using UnityEngine;

// No Canvas da fase: objeto "ObjectiveHUD" com CanvasGroup + TMP_Text filho + este script.
public class ObjectiveHUD : MonoBehaviour
{
    public static ObjectiveHUD Instance { get; private set; }

    [SerializeField] private TMP_Text objectiveText;
    [SerializeField] private CanvasGroup group;
    [SerializeField, TextArea] private string initialObjective = "Saia do castelo";
    [SerializeField] private float fadeTime = 0.8f;
    [SerializeField] private float visibleTime = 6f;   // 0 = fica sempre na tela
    private Coroutine running;

    void Awake() { Instance = this; }
    void Start() { SetObjective(initialObjective); }

    public void SetObjective(string text)
    {
        objectiveText.text = text;
        ShowObjective();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ShowObjective();
        }
    }

    public void ShowObjective()
    {
        if (running != null)
            StopCoroutine(running);

        running = StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return Fade(group.alpha, 1f);
        if (visibleTime <= 0f) yield break;
        yield return new WaitForSeconds(visibleTime);
        yield return Fade(1f, 0f);
    }

    IEnumerator Fade(float from, float to)
    {
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            group.alpha = Mathf.Lerp(from, to, t / fadeTime);
            yield return null;
        }
        group.alpha = to;
    }
}
