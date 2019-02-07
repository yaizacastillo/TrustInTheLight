using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    private int gameIndex = 0;

    public GameObject arrow1, arrow2;

    private void Start()
    {
        arrow1.SetActive(true);
        arrow2.SetActive(false);
    }

    void Update ()
    {
        if (XInputTestCS.Instance.statePlayer1.DPad.Down == XInputDotNetPure.ButtonState.Pressed)
        {
            gameIndex++;
            if (gameIndex >= 1)
            {
                gameIndex = 1;
            }

            arrow1.SetActive(false);
            arrow2.SetActive(true);
        }

        if (XInputTestCS.Instance.statePlayer1.DPad.Up == XInputDotNetPure.ButtonState.Pressed)
        {
            gameIndex--;
            if (gameIndex <= 0)
            {
                gameIndex = 0;
            }

            arrow1.SetActive(true);
            arrow2.SetActive(false);
        }

        if (Input.GetButtonDown("AButton"))
        {
            Debug.Log("X pressed");
            Action();
        }
    }

    public void Action()
    {
        if (gameIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Application.Quit();
        }
    }

}
