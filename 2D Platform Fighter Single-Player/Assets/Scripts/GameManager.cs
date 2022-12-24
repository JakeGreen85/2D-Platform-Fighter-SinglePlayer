using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameStart;
    public GameObject MenuUI;
    public GameObject PauseMenu;
    public GameObject GameOver;
    public GameObject OptionsMenu;
    public GameObject player;
    private GameState lastState;
    public GameState currentState;
    // Start is called before the first frame update
    
    void Start()
    {
        GameStart = false;
        currentState = GameState.MainMenu;
        HideAllUI();
    }

    void Update() {
        switch (currentState)
        {
            case GameState.MainMenu:
                if(!MenuUI.active) MenuUI.SetActive(true);
                break;
            case GameState.Options:
                if(!OptionsMenu.active) OptionsMenu.SetActive(true);
                break;
            case GameState.GameRunning:
                CheckUpdate();
                break;
            case GameState.GamePaused:
                if(!PauseMenu.active) PauseMenu.SetActive(true);
                break;
            case GameState.GameOver:
                if(!GameOver.active) GameOver.SetActive(true);
                break;
            default:
                Debug.Log("Not a valid state");
                break;
        }
    }

    void HideAllUI(){
        MenuUI.SetActive(false);
        PauseMenu.SetActive(false);
        GameOver.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    void CheckUpdate(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            ChangeState(GameState.GamePaused);
        }

    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Options(){
        ChangeState(GameState.Options);
    }

    public void StartGame(){
        ChangeState(GameState.GameRunning);
        Instantiate(player);
    }

    public void MainMenu(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject p in players){
            Destroy(p);
        }
        ChangeState(GameState.MainMenu);
    }

    public void LeaveOptionsMenu(){
        ChangeState(lastState);
    }

    public void ResumeGame(){
        ChangeState(GameState.GameRunning);
    }

    public void ChangeState(GameState newState){
        HideAllUI();
        lastState = currentState;
        currentState = newState;
    }

    public enum GameState
    {
        MainMenu,
        GameRunning,
        GamePaused,
        GameOver,
        Options
    }
}
