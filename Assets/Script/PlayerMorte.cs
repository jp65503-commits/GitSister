using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMorte : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private GameObject player;

    [Header("Ajustes do respawn")]
    [SerializeField] private float respawnTime;

    [Header("Verificações")]
    [SerializeField] public bool dead;
    [SerializeField] public bool respawned;

   private void Update()
    {
        if (dead && respawnTime > 0)
        {
            respawnTime -= Time.deltaTime;
        }
        else if (dead && respawnTime <= 0 && !respawned)
        {
            respawned = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void KillPlayer()
    {
        if (!dead)
        {
            dead = true;
            Instantiate(deathParticles, player.transform.position, deathParticles.transform.rotation);
            Camera.main.transform.SetParent(null);
            Destroy(player);
        }
    }
}