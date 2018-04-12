using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class persistOnLevels : MonoBehaviour {
    public string[] levelNames;
    public int[] levelInts;
    int initialLevel;
    // Use this for initialization
    bool original = false;
	void OnEnable () {
        persistOnLevels[] tracks = FindObjectsOfType<persistOnLevels>();
        foreach (persistOnLevels track in tracks) {
            if (original && track.gameObject != gameObject && track.gameObject.GetComponent<AudioSource>().clip == gameObject.GetComponent<AudioSource>().clip) {
                Destroy(track.gameObject);
            }
        }
        original = true;
        SceneManager.sceneLoaded += levelLoaded;
        //initialLevel = SceneManager.GetActiveScene().buildIndex;
        DontDestroyOnLoad(gameObject);

	}

    void Awake() {

    }

    void levelLoaded(Scene scene, LoadSceneMode mode) {
        persistOnLevels[] tracks = FindObjectsOfType<persistOnLevels>();
        foreach (persistOnLevels track in tracks) {
            if (original && track.gameObject != gameObject && track.gameObject.GetComponent<AudioSource>().clip == gameObject.GetComponent<AudioSource>().clip) {
                Destroy(track.gameObject);
            }
        }

        if (!original) Destroy(gameObject);
        int level = scene.buildIndex;
        //if (level != initialLevel) {
        //    return;
        //}
        foreach (string l in levelNames) {
            if (SceneManager.GetSceneByName(l) == SceneManager.GetActiveScene()) {
                return;
            }
        }

        foreach (int l in levelInts) {
            if (SceneManager.GetSceneByBuildIndex(l) == SceneManager.GetActiveScene()) {
                return;
            }
        }

        Destroy(gameObject);
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= levelLoaded;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
