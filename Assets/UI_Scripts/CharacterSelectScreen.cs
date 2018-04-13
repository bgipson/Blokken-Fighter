using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectScreen : MonoBehaviour
{
    public bool player1Ready;
    public bool player2Ready;

    public void Start()
    {
        FighterSetup[] setups = FindObjectsOfType<FighterSetup>();
        if (setups.Length > 0)
        {
            foreach (FighterSetup setup in setups)
            {
                Destroy(setup.gameObject);
            }
        }
    }

    public void ReadyOff(string player) {
        if (player == "Player2") {
            player2Ready = false;
        } else {
            player1Ready = false;
        }
    }
    public void ReadyUp(string player)
    {
        if (player == "Player1")
        {
            player1Ready = true;
        }
        else
        {
            player2Ready = true;
        }

        if (player1Ready && (RoundManager.player2AI || player2Ready))
        {
            if (RoundManager.player2AI == true) {
                GameObject randomExport = new GameObject("Player2");
                print("AYA");
                Dictionary<string, int>  setup = new Dictionary<string, int>();
                setup["Jab_1_Num"] = Random.Range(0, 3);
                setup["Jab_2_Num"] = Random.Range(0, 3);
                setup["Jab_3_Num"] = Random.Range(0, 3);
                setup["Up_Tilt_Num"] = Random.Range(0, 3);
                setup["Up_Air_Num"] = Random.Range(0, 3);
                setup["Down_Air_Num"] = Random.Range(0, 3);
                FighterSetup export = randomExport.AddComponent<FighterSetup>();
                export.name = "Player2";
                export.setup = setup;
                DontDestroyOnLoad(randomExport);
            }
            StartCoroutine(playersReady(2));
        }
    }

    IEnumerator playersReady(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        //RoundManager.setScreen();
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("StageSelect");
    }
}
