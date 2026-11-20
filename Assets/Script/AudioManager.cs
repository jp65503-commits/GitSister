using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource audioSourceSFX;

    [Header("Efeitos Sonoros")]
    [SerializeField] public AudioClip puloSFX;
    [SerializeField] public AudioClip botaoAtivoSFX;
    [SerializeField] public AudioClip botaoDesativoSFX;
    public void TocarSFX(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
    }
}