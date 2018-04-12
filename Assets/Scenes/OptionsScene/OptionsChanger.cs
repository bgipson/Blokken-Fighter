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

    public Slider[] sliders;
    public Slider p1_Input;
    public Text p1_text;
    public Slider p2_Input;
    public Text p2_text;
    private string[] windows = { "WINDOWED", "FULLSCREEN" };
    private string[] controller = { "XBOX 360", "PS4" };
	// Use this for initialization
	void Start () {
        p1_Input.value = PlayerPrefs.GetInt("Player1_Controller");
        p1_text.text = controller[PlayerPrefs.GetInt("Player1_Controller")];

        p2_Input.value = PlayerPrefs.GetInt("Player2_Controller");
        p2_text.text = controller[PlayerPrefs.GetInt("Player2_Controller")];

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

    public void changeInput(Slider s) {
        if (s.name == "P1_Window") {
            switch (Mathf.FloorToInt(s.value)) {
                case 0:
                    PlayerPrefs.SetInt("Player1_Controller", 0);
                    p1_text.text = controller[PlayerPrefs.GetInt("Player1_Controller")];
                    break;
                case 1:
                    PlayerPrefs.SetInt("Player1_Controller", 1);
                    p1_text.text = controller[PlayerPrefs.GetInt("Player1_Controller")];
                    break;
                default:
                    PlayerPrefs.SetInt("Player1_Controller", 0);
                    p1_text.text = controller[PlayerPrefs.GetInt("Player1_Controller")];
                    break;
            }
        }

        if (s.name == "P2_Window") {
            switch (Mathf.FloorToInt(s.value)) {
                case 0:
                    PlayerPrefs.SetInt("Player2_Controller", 0);
                    p2_text.text = controller[PlayerPrefs.GetInt("Player2_Controller")];
                    break;
                case 1:
                    PlayerPrefs.SetInt("Player2_Controller", 1);
                    p2_text.text = controller[PlayerPrefs.GetInt("Player2_Controller")];
                    break;
                default:
                    PlayerPrefs.SetInt("Player2_Controller", 0);
                    p2_text.text = controller[PlayerPrefs.GetInt("Player2_Controller")];
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
