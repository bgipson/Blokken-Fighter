using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OptionsChanger : MonoBehaviour {
    public EventSystem eventSystem;
    //public GameSettings gameSettings;

    public AudioSource bgm;

    private Slider[] sliders;
    private string[] windows = { "WINDOWED", "FULLSCREEN" };
	// Use this for initialization
	void Start () {
        sliders = GetComponentsInChildren<Slider>();

        foreach (Slider slider in sliders)
        {
            switch (slider.name) {
                case "Window":
                    if (Screen.fullScreen == false)
                    {
                        slider.GetComponentInChildren<Text>().text = windows[0];
                    }
                    else
                    {
                        slider.GetComponentInChildren<Text>().text = windows[1];
                    }
                    break;
                case "Volume":
                    slider.value = Mathf.FloorToInt(PlayerPrefs.GetFloat("Volume", 0.4f) * 5f);
                    break;
        }
            
        }
    }

    public void optionsPressed()
    {

    }

    public void returnToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    // Update is called once per frame
    void Update () {
		
	}

    public void changeOption(Slider s)
    {
        switch (s.name)
        {
            case "Volume":
                bgm.volume = s.value / 5f;
                PlayerPrefs.SetFloat("Volume", bgm.volume);
                break;
            case "Window":
                s.GetComponentInChildren<Text>().text = windows[Mathf.FloorToInt(s.value)];
                Screen.fullScreen = !Screen.fullScreen;


                break;
        }
    }
}
