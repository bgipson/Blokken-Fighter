using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class stageSelect : MonoBehaviour {
    public int stageNum = 0;
    public int[] stageLevels;
    public string[] stageNames;
    public GameObject[] stages;
    public VideoPlayer[] videos;

    public OutlineText stageSelectText;
	// Use this for initialization
	void Start () {
        videos[stageNum].Play();
	}

    // Update is called once per frame
    bool released = true;

	void Update () {
        if (released && Input.GetAxis("Horizontal_1P") > 0.3f) {
            if (stageNum + 1 < stages.Length) {
                videos[stageNum].Stop();
                stageNum += 1;
                videos[stageNum].Play();
            }
            released = false;
        } else if (released && Input.GetAxis("Horizontal_1P") < -0.3f) {
            if (stageNum - 1 >= 0) {
                videos[stageNum].Stop();
                stageNum -= 1;
                videos[stageNum].Play();
            }
            released = false;
        } else if (Mathf.Abs(Input.GetAxis("Horizontal_1P")) <= 0.3f) {
            released = true;
        }
        //print(Mathf.Abs(Input.GetAxis("Horizontal_1P")));

        if (Input.GetButtonDown("Jump_1P")) {
            RoundManager.player1Wins = 0;
            RoundManager.player2Wins = 0;
            SceneManager.LoadScene(stageLevels[stageNum]);
        }
        stageSelectText.mainText.text = stageNames[stageNum];
        Vector3 pos = transform.position;
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, stages[stageNum].transform.position.z), 0.1f);
        //transform.position = new Vector3(pos.x, pos.y, stages[stageNum].transform.position.z);
    }
}
