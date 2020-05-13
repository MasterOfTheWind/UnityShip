using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pause : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    
    void Update(){
        if (Input.GetButtonDown("Pause"))
            if (GameIsPaused)
                ResumeGame();
            
            else
                pauseGame();
        if (Input.GetButtonDown("Cancel"))
            if (GameIsPaused)
                ResumeGame();
    }


    public void ResumeGame() {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    private void pauseGame() {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame(){
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
