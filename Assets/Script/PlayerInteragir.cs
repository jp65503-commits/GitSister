using UnityEngine;

public class PlayerInteragir : MonoBehaviour
{

    [Header("Referências")]
    [SerializeField] private PlayerMorte playerMorte;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Dano"))
        {
            playerMorte.KillPlayer();
        }
    }
}
