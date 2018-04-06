using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    public Slider volumeSlider;
    public AudioSource BGM;
    public GameSettings gameSettings;
    public Button applyButton;


    private void OnEnable()
    {
        
        volumeSlider.onValueChanged.AddListener(delegate { OnVolumeChange(); });

        applyButton.onClick.AddListener(delegate { OnApplyButtonClicked(); });
    }

    public void OnVolumeChange()
    {
        BGM.volume = volumeSlider.value;
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
