using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject OptionsButton;
    public GameObject DropDownMenu;
    public GameObject BackButton;

    // Start is called before the first frame update
    void Start()
    {
        DropDownMenu.SetActive(false);
        BackButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void DisplayOptions()
    {
        StartButton.SetActive(false);
        OptionsButton.SetActive(false);

        DropDownMenu.SetActive(true);
        BackButton.SetActive(true);
    }

    public void GoBack()
    {
        StartButton.SetActive(true);
        OptionsButton.SetActive(true);

        DropDownMenu.SetActive(false);
        BackButton.SetActive(false);
    }
}
