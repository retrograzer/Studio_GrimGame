using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer audioMixer; // assign audio mixer. Second mixer can be added later if we want separate controls for sfx / music
    public Slider volumeSlider;
    public string mixerParameterName = "Volume";

    private const string volumeKey = "VolumeLevel";

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(volumeKey, .5f);
        SetVolume(volumeSlider.value);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat(mixerParameterName, Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat(volumeKey, volume);
    }
}
