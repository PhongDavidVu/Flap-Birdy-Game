using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject pauseUI;
    public GameObject deathUI;

    public GameObject removeCompnent;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape") && Collision.IsInputEnabled)
        {
            if (paused) Resume();
            else Pause();
            
        }
        if (!Collision.IsInputEnabled)
        {
        
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            removeCompnent.SetActive(false);
            deathUI.SetActive(true);
            if (Input.GetKeyDown("r")) Restart();
        }
    }

    public void Resume ()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f; 
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Pause ()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Menu ()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = 1f;
        Collision.IsInputEnabled = true;
        paused = false;
    }
    public void Restart ()
    {
        Environment.spawned = 0;
      
        removeCompnent.SetActive(true);
       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        Collision.IsInputEnabled = true;
        FPSControl.gunOn = false;
        
    }

    public void Quiting ()
    {
        Application.Quit();
    }
}
