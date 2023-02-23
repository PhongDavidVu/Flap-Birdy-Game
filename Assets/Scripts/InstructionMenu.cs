using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InstructionMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject toInclude;


    public void Back() {
        mainMenu.SetActive(true);
        toInclude.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
