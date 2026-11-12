using UnityEngine;
using UnityEngine.SceneManagement;

public class Teletransporte : MonoBehaviour
{
    [Header("Ajustes da Troca de fase")]
    [SerializeField] private string nomeDaFase;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName:nomeDaFase);
        }
    }
}

