using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverButtons : MonoBehaviour
{
    public void reloadScene()
    {
        Debug.Log(Time.timeScale);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log(Time.timeScale);
        
    }
}
