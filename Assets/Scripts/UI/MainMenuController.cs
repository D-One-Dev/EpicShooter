using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, soundSlider;
    [SerializeField] private AudioMixerGroup musicMixer, soundMixer;

    private float musicVolume, soundVolume;

    private void Start()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        soundVolume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        if(musicSlider != null)
            musicSlider.value = musicVolume;
        if(soundSlider != null)
            soundSlider.value = soundVolume;

        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80f, 0f, musicVolume));
        soundMixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80f, 0f, soundVolume));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void ChangeMusicVolume()
    {
        musicVolume = musicSlider.value;
        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80f, 0f, musicVolume));
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
    }

    public void ChangeSoundVolume()
    {
        soundVolume = soundSlider.value;
        soundMixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80f, 0f, soundVolume));
        PlayerPrefs.SetFloat("SoundVolume", soundVolume);
    }
}
