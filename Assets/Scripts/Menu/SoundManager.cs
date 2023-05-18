using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    public Slider soundSlider; // Reference to the slider UI element


    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();

        }

        else
        {
            Load();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = soundSlider.value;
        Save();
    }

    public void Load()
    {
        soundSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", soundSlider.value);
    }
}
