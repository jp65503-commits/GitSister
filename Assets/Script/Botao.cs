using UnityEngine;

public class Botao : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PortaInteragivel portaInteragivel;

    [Header("Verificações")]
    [SerializeField] public bool ativo;
    [SerializeField] private int objetosDentro = 0;

    private void Awake()
    {
        audioManager = Camera.main.GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Caixa"))
        {
            objetosDentro++;

            if (!ativo)
            {
                ativo = true;
                audioManager.TocarSFX(audioManager.botaoAtivoSFX);
                portaInteragivel.AbrirPorta();
                Debug.Log("enter trigger!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Caixa"))
        {
            objetosDentro--;

            if (objetosDentro <= 0)
            {
                objetosDentro = 0;
                audioManager.TocarSFX(audioManager.botaoDesativoSFX);
                portaInteragivel.FecharPorta();
                ativo = false;
                Debug.Log("left trigger!");
            }
        }
    }
}