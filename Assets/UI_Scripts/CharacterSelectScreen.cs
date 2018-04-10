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

        if (player1Ready && player2Ready)
        {
            SceneManager.LoadScene(2);
        }
    }
}
