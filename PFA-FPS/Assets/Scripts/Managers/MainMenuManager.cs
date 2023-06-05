using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : Singleton<MainMenuManager>
{
    public GameObject MainMenu;
    public GameObject MenuOptions;
    public GameObject HowToPlay;
    public GameObject Credits;
    public GameObject Selection;
    public CurrentPlayerMiddleMan currentPlayerMiddleMan;

    [Space]
    public GameObject gameplayPanel;
    public GameObject commandsPanel;
    public GameObject lobbyPanel;


    public void PanelSwap(string Panel)
    {
        if (Panel == "Transition1")
        {
            gameplayPanel.SetActive(false);
            commandsPanel.SetActive(true);
        }
        if (Panel == "Transition2")
        {
            gameplayPanel.SetActive(true) ;
            commandsPanel.SetActive(false);
        }
        if (Panel == "Transition3")
        {
            commandsPanel.SetActive(false);
            lobbyPanel.SetActive(true);
        }
        if (Panel == "Transition4")
        {
            commandsPanel.SetActive(true);
            lobbyPanel.SetActive(false);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ButtonClicked(string _String)
    {

        if (_String == "Options")
        {
            MainMenu.SetActive(false);
            MenuOptions.SetActive(true);
        }
        if (_String == "Return Options")
        {
            MainMenu.SetActive(true);
            MenuOptions.SetActive(false);
        }
        if (_String == "Credits")
        {
            MainMenu.SetActive(false);
            Credits.SetActive(true);
        }
        if (_String == "Return Credits")
        {
            MainMenu.SetActive(true);
            Credits.SetActive(false);
        }
        if (_String == "HowToPlay")
        {
            MainMenu.SetActive(false);
            HowToPlay.SetActive(true);
        }
        if (_String == "Return HowToPlay")
        {
            MainMenu.SetActive(true);
            HowToPlay.SetActive(false);
        }

        if (_String == "Selection")
        {
            MainMenu.SetActive(false);
            Selection.SetActive(true);
        }
        if (_String == "ReturnSelection")
        {
            MainMenu.SetActive(true);
            Selection.SetActive(false);
        }

    }

    public void PlayerSelected(string _name)
    {
        currentPlayerMiddleMan.SetCurrentPlayerTo(name);
        PlayerPrefs.SetString("SelectedPlayer", _name);
        SceneManager.LoadScene("Lobby");
    }
}
