using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {

    public CustomButton selectedFirst;
    public string verticalAxis;
    public string horizontalAxis;
    public string selectButton;
    private CustomButton currentButton;
    private bool centeredVertical = false;
    private bool centeredHorizontal = false;

	void Start ()
    {
        selectedFirst.Select();
        currentButton = selectedFirst;
	}

    private void Update()
    {
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
