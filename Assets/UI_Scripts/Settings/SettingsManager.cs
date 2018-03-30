using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour {

    public Slider volumeSlider;
    //public Toggle fullscreenToggle;
    public Dropdown fullscreenOption;
    public AudioSource BGM;
    public GameSettings gameSettings;
    public Button applyButton;
    

    private void OnEnable()
    {
        gameSettings = new GameSettings();
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
        fullscreenOption.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });

        applyButton.onClick.AddListener(delegate { OnApplyButtonClicked(); });
    }

    public void OnFullscreenToggle()
    {
        if (fullscreenOption.value == 0) { Screen.fullScreen = true; }
        else { Screen.fullScreen = false; }
        gameSettings.fullscreen = Screen.fullScreen;
    }

    public void OnVolumeChange()
    {
        BGM.volume = volumeSlider.value;
        gameSettings.volume = volumeSlider.value;
    }

    public void OnApplyButtonClicked()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {

    }
}
