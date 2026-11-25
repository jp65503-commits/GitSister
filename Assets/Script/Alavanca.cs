
using System.Collections;
using TMPro;
using UnityEngine;

public class Alavanca : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject pivotAlavanca;
    [SerializeField] private GameObject canvasAlavanca;
    [SerializeField] private TMP_Text textoAlavanca;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ScriptPlataformaMovel plataformaMovel;

    [Header("Verificacoes")]
    [SerializeField] private bool interagivel;
    [SerializeField] private bool ativada;

    [Header("Configuracoes")]
    [SerializeField] private float velocidade;

    private Coroutine movimentoAtual;

    private void Awake()
    {
        audioManager = Camera.main.GetComponent<AudioManager>();

        pivotAlavanca.transform.localRotation = Quaternion.Euler(90f, -45f, 0f);
    }

    private void Update()
    {
        if (interagivel && Input.GetKeyDown(KeyCode.E))
        {
            ativada = !ativada;

            if (ativada)
            {
                textoAlavanca.text = "\"E\" Para Destivar";
                audioManager.TocarSFX(audioManager.alavancaAtivaSFX);
                plataformaMovel.ativo = true;
            }
            else
            {
                textoAlavanca.text = "\"E\" Para Ativar";
                audioManager.TocarSFX(audioManager.alavancaDesativaSFX);
                plataformaMovel.ativo = false;
            }

            if (movimentoAtual != null) StopCoroutine(movimentoAtual);

            movimentoAtual = StartCoroutine(MoverAlavanca());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interagivel = true;
            canvasAlavanca.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interagivel = false;
            canvasAlavanca.SetActive(false);
        }
    }

    private IEnumerator MoverAlavanca()
    {
        Quaternion rotacaoInicial = pivotAlavanca.transform.localRotation;

        Quaternion rotacaoDestino = Quaternion.Euler(90f, ativada ? 45f : -45f, 0f);

        float tempo = 0f;

        while (tempo < 1f)
        {
            tempo += Time.deltaTime * velocidade;

            pivotAlavanca.transform.localRotation = Quaternion.Lerp(rotacaoInicial, rotacaoDestino, tempo);

            yield return null;
        }

        pivotAlavanca.transform.localRotation = rotacaoDestino;
        movimentoAtual = null;
    }
}