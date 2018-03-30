using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomChooser : CustomButton
{
    [System.Serializable]
    public class ChoiceChangedEvent : UnityEvent<string> { };

    public ChoiceChangedEvent choiceChanged;

    public string prompt;
    public string[] choices;
    public int currentChoice;
    private Text textbox;
    private Animator animator;

    private void Start()
    {
        choiceChanged = new ChoiceChangedEvent();
        currentChoice = 0;
        textbox = GetComponentInChildren<Text>();
        animator = GetComponent<Animator>();
    }

    private void ChoiceChanged(string choice)
    {
        textbox.text = prompt + ": <" + choice + ">";
        choiceChanged.Invoke(choice);
        animator.SetTrigger("Changed");
    }

    override public void AxisInput(float axis)
    {
        if (axis > 0.5f)
        {
            currentChoice = currentChoice + 1 < choices.Length ? currentChoice + 1 : 0;
            ChoiceChanged(choices[currentChoice]);
        }
        else if (axis < -0.5f)
        {
            currentChoice = currentChoice - 1 >= 0 ? currentChoice - 1 : choices.Length - 1;
            ChoiceChanged(choices[currentChoice]);
        }
    }
}
