using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {
    public Text[] options;
    public GameObject[] focuses;
    public string[] subTextStrings;
    public Vector3[] camPositions;
    public int[] levelSelect;
    public charSelectCam cam;
    public Text subText;
    int i = 0;
    // Use this for initialization
    void Start() {
        if (options.Length > 0) {
            defaultFontSize = options[0].fontSize;
            increaseFont(options[0], 60);
            cam.gameObject.transform.position = camPositions[0];
        }
        if (PlayerPrefs.GetInt("Player1_Controller") == 0) {
            PlayerInput.changeToXbox(1);
        } else if (PlayerPrefs.GetInt("Player1_Controller") == 1) {
            PlayerInput.changeToPS4(1);
        }

        if (PlayerPrefs.GetInt("Player2_Controller") == 0) {
            // PlayerInput.changeToXbox(2);
            PlayerInput.changeToPS4(2);
        } else if (PlayerPrefs.GetInt("Player2_Controller") == 1) {
            PlayerInput.changeToPS4(2);
        }
    }

    // Update is called once per frame
    bool shifted = false;

    IEnumerator changeScreen() {
        yield return new WaitForEndOfFrame();
        RoundManager.setScreen();
        SceneManager.LoadScene(levelSelect[i]);
    }
    void Update() {
        //if (Input.GetKeyDown(KeyCode.F1)) {
        //    SceneManager.LoadScene("Beach-Film");
        //}
        //if (Input.GetKeyDown(KeyCode.F2)) {
        //    SceneManager.LoadScene("Highway-Film");
        //}
        //if (Input.GetKeyDown(KeyCode.F3)) {
        //    SceneManager.LoadScene("TV-Film");
        //}

        if (Input.GetKeyDown(KeyCode.Escape)) {
            RoundManager.exitGame();
        }

        if (transitioning) {
            changeCam();
        }

        if (Input.GetButtonDown("Jump_1P") || Input.GetButtonDown(PlayerInput.jump_2p) || Input.GetButtonDown("Start_1P")) {
            StartCoroutine(changeScreen());
           
        }

        if (Mathf.Abs(Input.GetAxis("Vertical_1P")) < 0.05f) {
            shifted = false;
        }

		if (!shifted && Input.GetAxis("Vertical_1P") < -0.5f || Input.GetKeyDown(KeyCode.UpArrow)) {
            options[i].color = Color.white;
            returnFontSize(options[i]);
            i = i - 1;
            if (i < 0) i = options.Length - 1;
            options[i].color = Color.red;
            increaseFont(options[i], 60);
            subText.text = subTextStrings[i];
            shifted = true;
            changeCam();
        }

        if (!shifted && Input.GetAxis("Vertical_1P") > 0.5f || Input.GetKeyDown(KeyCode.DownArrow)) {
            options[i].color = Color.white;
            returnFontSize(options[i]);
            i = i + 1;
            if (i >= options.Length) i = 0;
            options[i].color = Color.red;
            increaseFont(options[i], 60);
            subText.text = subTextStrings[i];
            shifted = true;
            changeCam();
        }
	}

    int defaultFontSize;
    void increaseFont(Text text, int fontSize) {
        text.transform.parent.GetComponent<Text>().fontSize = fontSize;
        text.fontSize = fontSize;
    }

    void returnFontSize(Text text) {
        text.transform.parent.GetComponent<Text>().fontSize = defaultFontSize;
        text.fontSize = defaultFontSize;
    }

    bool transitioning = false;
    void changeCam() {
        transitioning = true;
        cam.rotationSpeed = 0;
        cam.transform.position = Vector3.Slerp(cam.transform.position, camPositions[i], 0.1f);
        cam.transform.rotation = Quaternion.Euler(Vector3.Slerp(cam.transform.rotation.eulerAngles, Vector3.zero, 0.1f));
        if (Vector3.Distance(cam.transform.position, camPositions[i]) < 0.1f) {
            transitioning = false;
            cam.transform.position = camPositions[i];
            cam.target = focuses[i];
            cam.rotationSpeed = 0.4f;
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
