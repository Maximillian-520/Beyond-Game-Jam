using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsController : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Toggle musicMuteToggle;
    [SerializeField] private Toggle sfxMuteToggle;

    [Header("Initial Settings")]
    [SerializeField] private float musicVolume = 1.0f;
    [SerializeField] private float sfxVolume = 1.0f;
    [SerializeField] private bool isMusicMute = false;
    [SerializeField] private bool isSfxMute = false;

    // ====================================================================================================
    //                     Virtual Methods
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Connect UI events
        if (musicVolumeSlider) musicVolumeSlider.onValueChanged.AddListener(
            value =>
            {
                musicVolume = value;
                SaveSettings();
            }
        );
        if (sfxVolumeSlider) sfxVolumeSlider.onValueChanged.AddListener(
            value =>
            {
                sfxVolume = value;
                SaveSettings();
            }
        );
        if (musicMuteToggle) musicMuteToggle.onValueChanged.AddListener(
            value =>
            {
                isMusicMute = value;
                SaveSettings();
            }
        );
        if (sfxMuteToggle) sfxMuteToggle.onValueChanged.AddListener(
            value =>
            {
                isSfxMute = value;
                SaveSettings();
            }
        );
        // Initialize
        LoadSettings();
    }
    #endregion

    // ====================================================================================================
    //                     Settings Methods
    // ====================================================================================================
    #region Settings
    private void ApplySettings()
    {
        // Check audio manager
        if (!AudioManager.Instance) return;
        // Set volume
        AudioManager.Instance.SetMusicVolume(musicVolume);
        AudioManager.Instance.SetSFXVolume(sfxVolume);
        // Set mute
        AudioManager.Instance.SetMusicMute(isMusicMute);
        AudioManager.Instance.SetSFXMute(isSfxMute);
    }

    public void SaveSettings()
    {
        // Save settings
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetInt("MuteMusic", isMusicMute ? 1 : 0);
        PlayerPrefs.SetInt("MuteSFX", isSfxMute ? 1 : 0);
        PlayerPrefs.Save();
        // Apply new saved changes
        ApplySettings();
    }

    public void LoadSettings()
    {
        // Load settings
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        isMusicMute = PlayerPrefs.GetInt("MusicMute", 0) == 1;
        isSfxMute = PlayerPrefs.GetInt("SFXMute", 0) == 1;
        // Apply settings
        ApplySettings();
        // Set UI values
        if(musicVolumeSlider) musicVolumeSlider.value = musicVolume;
        if(sfxVolumeSlider) sfxVolumeSlider.value = sfxVolume;
        if(musicMuteToggle) musicMuteToggle.isOn = isMusicMute;
        if(sfxMuteToggle) sfxMuteToggle.isOn = isSfxMute;
    }
    #endregion
}
