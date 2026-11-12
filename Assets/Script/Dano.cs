using UnityEngine;
using UnityEngine.SceneManagement;

public class Dano : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject playerVisual;

    [Header("Ajustes de Respawn")]
    [SerializeField] private float tempoAteRespawn = 2f;
    private float timerRespawn;

    [Header("Estado")]
    public bool dead { get; private set; }

    private void Start()
    {
        timerRespawn = tempoAteRespawn;
    }

    private void Update()
    {
        if (dead)
        {
            timerRespawn -= Time.deltaTime;

            if (timerRespawn <= 0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void KillPlayer()
    {
        if (dead) return;

        dead = true;

        if (deathParticles != null)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }

        if (playerVisual != null)
        {
            playerVisual.SetActive(false);
        }

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

    }
}