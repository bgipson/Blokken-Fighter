using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour {

    public CustomButton selectedFirst;
    public GameObject controller;
    public string verticalAxis;
    public string horizontalAxis;
    public string selectButton;
    private CustomButton currentButton;
    private bool centeredVertical = false;
    private bool centeredHorizontal = false;

    public Animator fighter;
    private Animator customizeAnimator;

    public string setupName;

    private Dictionary<string, int> setup;

	void Start ()
    {
        selectedFirst.Select();
        currentButton = selectedFirst;
        customizeAnimator = controller.GetComponent<Animator>();
        setup = new Dictionary<string, int>();
        SetAllToType("BOXER");
	}

    public void Ready()
    {
        currentButton.Deselect();
        currentButton = null;
        customizeAnimator.SetBool("Ready", true);
        GameObject exported = new GameObject(setupName);
        FighterSetup export = exported.AddComponent<FighterSetup>();
        export.name = setupName;
        export.setup = setup;
        DontDestroyOnLoad(exported);
        FindObjectOfType<CharacterSelectScreen>().ReadyUp(setupName);
    }

    public void LoadSettings()
    {
        Transform customize = controller.transform.Find("Customize");

        LoadSetting(customize, "Jab 1", "Jab_1_Num");
        LoadSetting(customize, "Jab 2", "Jab_2_Num");
        LoadSetting(customize, "Jab 3", "Jab_3_Num");
        LoadSetting(customize, "Up Tilt", "Up_Tilt_Num");
        LoadSetting(customize, "Up Air", "Up_Air_Num");
        LoadSetting(customize, "Down Air", "Down_Air_Num");
    }

    public void LoadSetting(Transform customize, string chooser, string key)
    {
        CustomChooser choiceButton = customize.Find(chooser).GetComponent<CustomChooser>();
        choiceButton.currentChoice = setup[key];
        choiceButton.ChoiceChanged(GetJabString(setup[key]));
    }

    public void SetAllToType(string type)
    {
        int setting = GetJabNum(type);
        setup["Jab_1_Num"] = setting;
        setup["Jab_2_Num"] = setting;
        setup["Jab_3_Num"] = setting;
        setup["Up_Tilt_Num"] = setting;
        setup["Up_Air_Num"] = setting;
        setup["Down_Air_Num"] = setting;
    }

    public void PlayJab(int num, int jab)
    {
        string key = "Jab_" + jab + "_Num";
        fighter.SetInteger(key, num);
        setup[key] = num;
    }

    public void SetJabNum(int num)
    {
        fighter.SetInteger("Jab_Num", num);
    }

    public void SetJab(bool jab)
    {
        fighter.SetBool("Jab", jab);
    }

    public void SetUpTilt(bool uptilt)
    {
        fighter.SetBool("UpTilt", uptilt);
    }

    public void SetUpTiltNum(string num)
    {
        fighter.SetInteger("Up_Tilt_Num", GetJabNum(num));
        setup["Up_Tilt_Num"] = GetJabNum(num);
    }

    public void SetUpAirNum(string num)
    {
        fighter.SetInteger("Up_Air_Num", GetJabNum(num));
        setup["Up_Air_Num"] = GetJabNum(num);
    }

    public void SetUpAir(bool upair)
    {
        fighter.SetBool("UpAir", upair);
    }

    public void SetDownAirNum(string num)
    {
        fighter.SetInteger("Down_Air_Num", GetJabNum(num));
        setup["Down_Air_Num"] = GetJabNum(num);
    }

    public void SetDownAir(bool upair)
    {
        fighter.SetBool("DownAir", upair);
    }

    private int GetJabNum(string type)
    {
        if (type == "BOXER" )
        {
            return 0;
        }
        else if (type == "KICKER")
        {
            return 1;
        }
        return 2;
    }

    private string GetJabString(int num)
    {
        if (num == 0)
        {
            return "BOXER";
        }
        else if (num == 1)
        {
            return "KICKER";
        }
        return "BERSERKER";
    }

    public void PlayJab1(string type)
    {
        PlayJab(GetJabNum(type), 1);
    }

    public void PlayJab2(string type)
    {
        PlayJab(GetJabNum(type), 2);
    }

    public void PlayJab3(string type)
    {
        PlayJab(GetJabNum(type), 3);
    }

    public void CustomizeScreen(bool customizing)
    {
        customizeAnimator.SetBool("Customizing", customizing);
        currentButton.Deselect();
        if (customizing)
        {
            LoadSettings();
            currentButton = controller.transform.Find("Customize/Jab 1").GetComponent<CustomButton>();
            currentButton.Select();
        }
        else
        {
            currentButton = controller.transform.Find("Main/Customize").GetComponent<CustomButton>();
            currentButton.Select();
        }
    }

    private void Update()
    {
        if (!currentButton)
        {
            return;
        }

        if (Input.GetButtonDown(selectButton))
        {
            currentButton.Activate();
        }

        if (Mathf.Abs(Input.GetAxisRaw(verticalAxis)) < 0.05f)
        {
            centeredVertical = true;
        }

        if (Mathf.Abs(Input.GetAxisRaw(horizontalAxis)) < 0.05f)
        {
            centeredHorizontal = true;
        }

        if (centeredVertical)
        {
            if (Input.GetAxisRaw(verticalAxis) > 0.5f)
            {
                currentButton = currentButton.SelectDown();
                centeredVertical = false;
                return;
            }

            if (Input.GetAxisRaw(verticalAxis) < -0.5f)
            {
                currentButton = currentButton.SelectUp();
                centeredVertical = false;
                return;
            }
        }

        if (centeredHorizontal && Mathf.Abs(Input.GetAxisRaw(horizontalAxis)) > 0.5f)
        {
            currentButton.AxisInput(Input.GetAxisRaw(horizontalAxis));
            centeredHorizontal = false;
        }
    }
}
