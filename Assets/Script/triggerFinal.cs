using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class triggerFinal : MonoBehaviour

{
    [Header("Verificacoes")]
    [SerializeField] private bool interagiu;
    [Header("Referencias")]
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private GameObject canvasFalaIrmao;
    [SerializeField] private GameObject canvasFinal;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PauseManager pauseManager;
    [SerializeField] private float tempoAteComecar;
    [SerializeField] private float tempoDeFala;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !interagiu)
        {
            Debug.Log("Interagiu");
            pauseManager.isInCutscene = true;
            interagiu = true;
            playerMove.cutscene = true;
            canvasFalaIrmao.gameObject.SetActive(true);
            StartCoroutine(EventoFinal());
        }
    }
    private IEnumerator EventoFinal()
    {
        yield return new WaitForSeconds(tempoAteComecar);
        canvasFalaIrmao.gameObject.SetActive(false);
        audioManager.TocarSFX(audioManager.diabo);
        yield return new WaitForSeconds(tempoDeFala);
        canvasFinal.gameObject.SetActive(true);
      

    }

    public void VoltarAoMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
