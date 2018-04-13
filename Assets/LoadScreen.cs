using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadScreen : MonoBehaviour {
    public float loadTime = 5f;
    public Text loadText;
	// Use this for initialization
	void Start () {
        StartCoroutine(waitToLoad());
	}
	
    IEnumerator waitToLoad() {
        yield return new WaitForSeconds(loadTime);
        yield return new WaitForEndOfFrame();
        RoundManager.setScreen();
        print("SNAPPED");
        SceneManager.LoadScene(PlayerPrefs.GetInt("NextLevel"));
    }
	// Update is called once per frame
	void Update () {
        int load = Mathf.FloorToInt(Time.time % 3);
        string loadString = "Loading";
        for (int i = 0; i <= load; i++) {
            loadString += ".";
        }
        loadText.text = loadString;
	}
}
