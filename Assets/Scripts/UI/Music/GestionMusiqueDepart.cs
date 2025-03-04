using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionMusiqueDepart : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] public AudioClip[] songs;
    public int volume = 1;
    [SerializeField] public int CurrentScene;
    [SerializeField] public int songtobeplayed;

    void Start()
    {
        _audioSource = FindObjectOfType<MusiqueFond>().GetComponent<AudioSource>();
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
        CurrentScene = songtobeplayed;

        ChangeSong(CurrentScene);

        if (songtobeplayed != CurrentScene)
        {
            songtobeplayed++;
            ChangeSong(songtobeplayed);
        }
    }

    void Update()
    {

        CurrentScene = SceneManager.GetActiveScene().buildIndex;

        if (songtobeplayed != CurrentScene)
        {
            songtobeplayed++;
            ChangeSong(songtobeplayed);
        }

    }

    public void ChangeSong(int songpicked)
    {
        _audioSource.clip = songs[songpicked];
        _audioSource.Play();
    }

    public void MusiqueOnOff()
    {

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
            _audioSource.volume = 1f;
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            _audioSource.volume = 0;
        }
    }
}
