using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _as;
    public static SoundController instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlaySound(AudioClip sound)
    {
        _as.PlayOneShot(sound);
    }
}
