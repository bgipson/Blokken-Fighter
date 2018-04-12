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
