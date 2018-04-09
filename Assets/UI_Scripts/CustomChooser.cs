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

    public string[] choices;
    public int currentChoice;
    private Text textbox;
    private Animator animator;
    private GameObject arrow;

    private void Awake()
    {
        currentChoice = 0;
        textbox = transform.Find("Choice").GetComponent<Text>();
        animator = GetComponent<Animator>();
        arrow = transform.Find("Arrow").gameObject;
    }

    public void ShowArrow(bool show)
    {
        arrow.SetActive(show);
    }

    public void ChoiceChanged(string choice)
    {
        textbox.text = choice;
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
