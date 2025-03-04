using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{

    [SerializeField] private InputActionAsset _actionsAsset = default;
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject controlMenuUI;
    [SerializeField] public GameObject gameOverMenuUI;


    private InputAction pause;
    private static bool pause_Active = false;
    private Player _player;
    private static bool anti_spam = false;  //band-aid fix to stop the spamming of the pause ui

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update() //Changes : added an anti-spam to prevent the screen to flicker if you tried to pause, and fixed a few things to actually make the ui show up when you tried to reopen the pause screen when you resumed.
    {
        if (Keyboard.current.escapeKey.IsPressed())
        {
            if (pause_Active && !anti_spam)
            {
                ResumeGame();
            }
            else if (!pause_Active && !anti_spam)
            {
                PauseGame();
            }
            anti_spam = true;
        }
        else if (!Keyboard.current.escapeKey.IsPressed())
        {
            anti_spam = false;
        }
        
        if (_player.estVivant = false)
        {
            GameOver();
        }

    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pause_Active = true;
    }

    public void GameOver()
    {
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        pause_Active = true;
    }

    public void ResumeGame()
    {
        if (controlMenuUI == true)
        {
            controlMenuUI.SetActive(false);
        }

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pause_Active = false;
    }
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneID);
    }


}