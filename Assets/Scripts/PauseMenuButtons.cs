using UnityEngine;


public class PauseMenuButtons : MonoBehaviour
{
    public Animator FadeAnimator;

    public void ResumeButton()
    {
        Time.timeScale = 1;
    }

    public void SettingsButton()
    {
    }


}
