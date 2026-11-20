using System.Collections;
using UnityEngine;

public class PortaInteragivel : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject portaEsquerda;
    [SerializeField] private GameObject portaDireita;
    [SerializeField] private AudioManager audioManager;

    [Header("Configuracoes")]
    [SerializeField] private float velocidade = 2f;

    private Quaternion esquerdaFechada;
    private Quaternion direitaFechada;

    private void Awake()
    {
        audioManager = Camera.main.GetComponent<AudioManager>();
    }

    private void Start()
    {
        esquerdaFechada = portaEsquerda.transform.localRotation;
        direitaFechada = portaDireita.transform.localRotation;
    }

    public void AbrirPorta()
    {
        StopAllCoroutines();
        StartCoroutine(MoverPorta(Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 180, 0)));
        audioManager.TocarSFX(audioManager.portaAbrirSFX);
    }

    public void FecharPorta()
    {
        StopAllCoroutines();
        StartCoroutine(MoverPorta(esquerdaFechada, direitaFechada));
        audioManager.TocarSFX(audioManager.portaFecharSFX);
    }

    private IEnumerator MoverPorta(Quaternion esquerdaDestino, Quaternion direitaDestino)
    {
        Quaternion esquerdaInicio = portaEsquerda.transform.localRotation;
        Quaternion direitaInicio = portaDireita.transform.localRotation;

        float tempo = 0;

        while (tempo < 1f)
        {
            tempo += Time.deltaTime * velocidade;

            portaEsquerda.transform.localRotation =
                Quaternion.Lerp(esquerdaInicio, esquerdaDestino, tempo);

            portaDireita.transform.localRotation =
                Quaternion.Lerp(direitaInicio, direitaDestino, tempo);

            yield return null;
        }
    }
}