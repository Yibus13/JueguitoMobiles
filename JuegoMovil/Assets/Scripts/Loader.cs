using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public void playLevel1()
    {
        StartCoroutine(LoadGame1());
        Time.timeScale = 1f;
    }
    IEnumerator LoadGame1()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Nivel 1");
    }
    public void playLevel2()
    {
        StartCoroutine(LoadGame2());
        Time.timeScale = 1f;
    }
    IEnumerator LoadGame2()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Nivel 2");
    }
    public void playLevel3()
    {
        StartCoroutine(LoadGame3());
        Time.timeScale = 1f;
    }
    IEnumerator LoadGame3()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Nivel 3");
    }
    public void playLevel4()
    {
        StartCoroutine(LoadGame4());
        Time.timeScale = 1f;
    }
    IEnumerator LoadGame4()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Nivel 4");
    }

    public void playMenu()
    {
        StartCoroutine(LoadMenu());
        Time.timeScale = 1f;
    }
    IEnumerator LoadMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("MainMenu");
    }
}
