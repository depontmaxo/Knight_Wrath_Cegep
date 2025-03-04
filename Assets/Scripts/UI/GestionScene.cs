using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScene : MonoBehaviour
{
    public int sceneCourante = 0;

    // But: Changer la sc�ne
    public void ChargerScene()
    {
        sceneCourante = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneCourante + 1);
    }

    // But: Revenir � la scene 0
    public void SceneMenu()
    {
        SceneManager.LoadScene(0);
    }

    // But: Revenir � la scene 1
    public void SceneHub()
    {
        SceneManager.LoadScene(1);
    }

    // But: Revenir � la scene tuto
    public void SceneTutorial()
    {
        SceneManager.LoadScene(7);
    }

    // But: Quitter le jeu
    public void Quitter()
    {
        Application.Quit();
    }

}
