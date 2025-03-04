using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{

    [SerializeField] private InputActionAsset _actionsAsset = default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private TextMeshProUGUI _txtGameOver = default;
    [SerializeField] private TextMeshProUGUI _txtRestart = default;
    [SerializeField] private TextMeshProUGUI _txtQuit = default;
    [SerializeField] private GameObject _pausePanel = default;
    [SerializeField] private GameObject _GameOver = default;


    private int _score;
    private bool _estChanger;
    //private AudioSource _musique;
    private bool _pauseOn = false;
    private InputAction pause;
    private InputAction restart;
    private Player _player;

    // Start is called before the first frame update

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        //_musique = FindObjectOfType<MusiqueFond>().GetComponent<AudioSource>();
        _score = 0;
        _estChanger = false;
        _pauseOn = false;

    }



    public void GameOverSequence()
    {
        _GameOver.SetActive(true);
        Time.timeScale = 0;
    }


    /* public void MusiqueOnOff()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
            _musique.volume = 0.078f;
            
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            _musique.volume = 0f;

        }
    }*/

}

