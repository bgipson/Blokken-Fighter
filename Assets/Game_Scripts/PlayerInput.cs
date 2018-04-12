using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInput {
    

    public static string  attack_1p = "Attack_1P";
    public static string jump_1p = "Jump_1P";
    public static string guard_1p = "Guard_1P";
    public static string dodge_left_1p = "Dodge_Left_1P";
    public static string dodge_right_1p = "Dodge_Right_1P";
    public static string start_1p = "Start_1P";
    public static string select_1p = "Select_1P";

    public static string attack_2p = "Attack_2P";
    public static string jump_2p = "Jump_2P";
    public static string guard_2p = "Guard_2P";
    public static string dodge_left_2p = "Dodge_Left_2P";
    public static string dodge_right_2p = "Dodge_Right_2P";
    public static string start_2p = "Start_2P";
    public static string select_2p = "Select_2P";

    public static void changeToXbox(int player = 1) {
        if (player == 1) {
            attack_1p = "Attack_1P";
            jump_1p = "Jump_1P";
            guard_1p = "Guard_1P";
            dodge_left_1p = "Dodge_Left_1P";
            dodge_right_1p = "Dodge_Right_1P";
            start_1p = "Start_1P";
            select_1p = "Select_1P";
        } else {
            attack_2p = "Attack_2P";
            jump_2p = "Jump_2P";
            guard_2p = "Guard_2P";
            dodge_left_2p = "Dodge_Left_2P";
            dodge_right_2p = "Dodge_Right_2P";
            start_2p = "Start_2P";
            select_2p = "Select_2P";
        }
    }

    public static void changeToPS4(int player = 2) {
        if (player == 1) {
            attack_1p = "PS3_Square_P1";
            jump_1p = "Attack_1P";
            guard_1p = "Guard_1P";
            dodge_left_1p = "Dodge_Left_1P";
            dodge_right_1p = "Dodge_Right_1P";
            start_1p = "PS3_Start_P1";
            select_1p = "PS3_Select_P1";
        } else {
            attack_2p = "PS3_Square_P2";
            jump_2p = "Attack_2P";
            guard_2p = "Guard_2P";
            dodge_left_2p = "Dodge_Left_2P";
            dodge_right_2p = "Dodge_Right_2P";
            start_2p = "PS3_Start_P2";
            select_2p = "PS3_Select_P2";
        }
    }
}
