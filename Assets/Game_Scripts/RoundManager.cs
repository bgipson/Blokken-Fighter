using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class RoundManager {
    public static int player1Wins = 0;
    public static int player2Wins = 0;

    public static int currentWinnerPlayer = 1; //The Player that won the last match

    public static int total_p1_wins = 0;
    public static int total_p2_wins = 0;

    public static int total_p1_combos = 0;
    public static int total_p2_combos = 0;

    public static int max_p1_combo = 0;
    public static int max_p2_combo = 0;

    public static int hits_taken_p1 = 0;
    public static int hits_taken_p2 = 0;

    public static Texture2D lastScreen = null;
    public static int screenWidth = 0;
    public static int screenHeight = 0;


    public static void captureScreen() {
        Object.Destroy(lastScreen);
        ScreenCapture.CaptureScreenshot("Transition.png");
        screenWidth = Screen.width;
        screenHeight = Screen.height;

    }

    public static void setScreen() {
        lastScreen = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        lastScreen.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        lastScreen.Apply();
    }

    public static Texture2D getScreen() {
        return lastScreen;
    }


}
