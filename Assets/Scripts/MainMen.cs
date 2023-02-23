using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMen : MonoBehaviour
{
    public GameObject instructionMenu;
    public GameObject toRemove;
    public void PlayGame () {
      
        Environment.spawned = 0;

        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        Collision.IsInputEnabled = true;
        FPSControl.gunOn = false;
        
    }

    public void Instruction() {
        instructionMenu.SetActive(true);
        toRemove.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
