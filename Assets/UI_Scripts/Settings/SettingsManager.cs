using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SettingsManager : MonoBehaviour {

    public Slider volumeSlider;
    //public Toggle fullscreenToggle;
    public Slider screen;
    public AudioSource BGM;
    public GameSettings gameSettings;
    public Button applyButton;
    

    private void OnEnable()
    {
        gameSettings = new GameSettings();
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });
        screen.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });

        applyButton.onClick.AddListener(delegate { OnApplyButtonClicked(); });
    }

    public void OnFullscreenToggle()
    {
        if (screen.value == 0) {
            Screen.fullScreen = true;

        }
        else { Screen.fullScreen = false; }
        gameSettings.fullscreen = Screen.fullScreen;
    }

    public void OnVolumeChange()
    {
        BGM.volume = volumeSlider.value / 0.2f;
        gameSettings.volume = volumeSlider.value / 0.2f;
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
