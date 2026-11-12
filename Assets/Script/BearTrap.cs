using UnityEngine;

public class BearTrap : MonoBehaviour
{
    [Header("Efeitos da Armadilha")]
    [SerializeField] private AudioClip snapSound;
    private AudioSource audioSource;
    private bool armadilhaDisparada = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (armadilhaDisparada) return;

        // Verifica se o objeto que pisou possui a tag Player
        if (other.CompareTag("Player"))
        {
            // Busca o componente de morte no objeto que colidiu
            PlayerMorte morte = other.GetComponent<PlayerMorte>();

            // Se o script estiver em um objeto pai ou filho
            if (morte == null)
            {
                morte = other.GetComponentInParent<PlayerMorte>();
            }

            if (morte != null)
            {
                armadilhaDisparada = true;

                // Toca som se houver AudioSource configurado
                if (audioSource != null && snapSound != null)
                {
                    audioSource.PlayOneShot(snapSound);
                }

                // Dispara a animação ou lógica de fechamento da armadilha no futuro
                // GetComponent<Animator>()?.SetTrigger("Snap");

                morte.KillPlayer();
            }
        }
    }
}
