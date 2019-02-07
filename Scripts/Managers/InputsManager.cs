using UnityEngine;
using XInputDotNetPure;

public class InputsManager : MonoSingleton<InputsManager> {

    public float vibrateReductionValue = 3;

    [HideInInspector]
    public float leftPlayer1Vibrator;
    [HideInInspector]
    public float rightPlayer1Vibrator;
    [HideInInspector]
    public float leftPlayer2Vibrator;
    [HideInInspector]
    public float rightPlayer2Vibrator;


    private void Update()
    {
        //leftPlayer1Vibrator = XInputTestCS.Instance.statePlayer1.Triggers.Left;
        //rightPlayer1Vibrator = XInputTestCS.Instance.statePlayer1.Triggers.Right;
        //leftPlayer2Vibrator = XInputTestCS.Instance.statePlayer2.Triggers.Left;
        //rightPlayer2Vibrator = XInputTestCS.Instance.statePlayer2.Triggers.Right;
    }

    #region Input Methods
    public void LeftTriggerPlayer1Value(float value)
    {
        leftPlayer1Vibrator = value;
    }

    public void RightTriggerPlayer1Value(float value)
    {
        rightPlayer1Vibrator = value;
    }

    public void LeftTriggerPlayer2Value(float value)
    {
        leftPlayer2Vibrator = value;
    }

    public void RightTriggerPlayer2Value(float value)
    {
        rightPlayer2Vibrator = value;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public float GetMovementX(bool isPlayer1)
    {
        return isPlayer1 ? XInputTestCS.Instance.statePlayer1.ThumbSticks.Left.X : XInputTestCS.Instance.statePlayer2.ThumbSticks.Left.X;
    }

    public float GetMovementY(bool isPlayer1)
    {
        return isPlayer1 ? XInputTestCS.Instance.statePlayer1.ThumbSticks.Left.Y : XInputTestCS.Instance.statePlayer2.ThumbSticks.Left.Y;
    }

    public bool GetActionButtonInputDown(bool isPlayer1)
    {
        return isPlayer1 ? XInputTestCS.Instance.statePlayer1.Buttons.A == ButtonState.Pressed : XInputTestCS.Instance.statePlayer2.Buttons.B == ButtonState.Pressed;
    }

    public bool GetStartButtonDown()
    {
        return Input.GetButtonDown("StartButton");
    }

    public bool GetHereButtonDown()
    {
        return XInputTestCS.Instance.statePlayer2.Buttons.A == ButtonState.Pressed;
    }

    public bool GetStopButtonDown()
    {
        return XInputTestCS.Instance.statePlayer2.Buttons.X == ButtonState.Pressed;
    }
    #endregion

}
