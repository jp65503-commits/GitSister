using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource audioSourceSFX;

    [Header("Efeitos Sonoros")]
    [SerializeField] public AudioClip puloSFX;
    [SerializeField] public AudioClip botaoAtivoSFX;
    [SerializeField] public AudioClip botaoDesativoSFX;
    [SerializeField] public AudioClip portaAbrirSFX;
    [SerializeField] public AudioClip portaFecharSFX;
    [SerializeField] public AudioClip alavancaAtivaSFX;
    [SerializeField] public AudioClip alavancaDesativaSFX;
    [SerializeField] public AudioClip diabo;

    public void TocarSFX(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
    }
}