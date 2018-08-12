using UnityEngine;

public class ButtonAnim : MonoBehaviour {

    public Animator mainMenuButton;
    public Animator categoryButton;
    public Animator mainMenuPanel;
    public Animator categoryPanel;

    void Start ()
    {
        mainMenuButton.Play("Pressed");
        mainMenuPanel.Play("Panel Open");
    }

    public void MainMenuClick ()
    {
        mainMenuButton.Play("Pressed");
        categoryButton.Play("Normal");
        mainMenuPanel.Play("Panel Open");
        categoryPanel.Play("Panel Close");
    }

    public void CategoryClick()
    {
        categoryButton.Play("Pressed");
        mainMenuButton.Play("Normal");
        categoryPanel.Play("Panel Open");
        mainMenuPanel.Play("Panel Close");
    }
}
