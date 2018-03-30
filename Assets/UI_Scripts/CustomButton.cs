using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour {

    [System.Serializable]
    public class SelectionChangedEvent : UnityEvent<bool> {};

    public CustomButton selectUp;
    public CustomButton selectDown;
    public SelectionChangedEvent selectionChanged;
    public UnityEvent onActivate;

	void Start ()
    {
        selectionChanged = new SelectionChangedEvent();
        onActivate = new UnityEvent();
	}

    void Activate()
    {
        onActivate.Invoke();
    }

    public void Select()
    {
        GetComponent<AnimatorInterface>().SetSelected(true);
        selectionChanged.Invoke(true);
    }

    public CustomButton SelectUp()
    {
        selectionChanged.Invoke(false);
        GetComponent<AnimatorInterface>().SetSelected(false);
        selectUp.Select();
        return selectUp;
    }

    public CustomButton SelectDown()
    {
        selectionChanged.Invoke(false);
        GetComponent<AnimatorInterface>().SetSelected(false);
        selectDown.Select();
        return selectDown;
    }

    virtual public void AxisInput(float axis) { }
}
