using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenuButtonFunctions : MonoBehaviour
{
    public Animator FadeAnimator;
    public void StartButton()
    {
        StartCoroutine(PlayFadeAndSwitch(1));
    }
    public void MainMenuButton()
    {
        StartCoroutine(PlayFadeAndSwitch(0));
    }

    IEnumerator PlayFadeAndSwitch(int sceneIndex)
    {
        Time.timeScale = 1;
        FadeAnimator.SetTrigger("Fade");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneIndex);
    }



}
