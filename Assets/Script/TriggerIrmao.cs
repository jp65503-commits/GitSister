using System.Collections;
using TMPro;
using UnityEngine;

public class TriggerIrmao : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject irmao;
    [SerializeField] private GameObject canvasFalaIrmao;
    [SerializeField] private PlayerMove playerMove;

    [Header("Verificacoes")]
    [SerializeField] private bool interagiu;

    [Header("Texto Irmao")]
    [SerializeField] private string falaDoIrmao;
    [SerializeField] private TMP_Text textMesh;

    [Header("Opçoes Da Animacao")]
    [SerializeField] private Animator irmaoAnimator;
    [SerializeField] private float tempoAteSair;
    [SerializeField] private float velocidade;
    [SerializeField] private float tempoAndando;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !interagiu)
        {
            Debug.Log("Interagiu");
            interagiu = true;
            playerMove.cutscene = true;
            canvasFalaIrmao.SetActive(true);
            textMesh.text = falaDoIrmao;
            StartCoroutine(IrmaoSair());
        }
    }

    private IEnumerator IrmaoSair()
    {
        yield return new WaitForSeconds(tempoAteSair);

        canvasFalaIrmao.SetActive(false);
        irmaoAnimator.SetBool("isWalking", true);
        irmao.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        float tempo = 0f;

        while (tempo < tempoAndando)
        {
            irmao.transform.position += irmao.transform.forward * velocidade * Time.deltaTime;

            tempo += Time.deltaTime;

            yield return null;
        }

        irmaoAnimator.SetBool("isWalking", false);
        Destroy(irmao);
        playerMove.cutscene = false;
        playerMove.canJump = true;
    }
}