using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Escena1");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
