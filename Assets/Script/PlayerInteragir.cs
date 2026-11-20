using TMPro;
using UnityEngine;

public class PlayerInteragir : MonoBehaviour
{

    [Header("Referências")]
    [SerializeField] private PlayerMorte playerMorte;
    [SerializeField] private GameObject canvasLivro;
    [SerializeField] private TextMeshProUGUI textoDaPaginaTMP;
    [SerializeField] private PauseManager pauseManager;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Dano"))
        {
            playerMorte.KillPlayer();
        }

        if (collision.CompareTag("livro"))
        {
            pauseManager.HandlePause(true);
            textoDaPaginaTMP.text = collision.GetComponent<Livro>().textoNovo;
            Destroy(collision.gameObject);
            SetLivroChildren(true);
        }
    }
    public void SetLivroChildren(bool active)
    {
        foreach (Transform child in canvasLivro.transform)
        {
            child.gameObject.SetActive(active);
        }

        if (!active)
        {
            pauseManager.FecharLivro();
        }
    }
}