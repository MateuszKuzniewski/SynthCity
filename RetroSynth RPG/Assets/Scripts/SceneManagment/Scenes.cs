using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenes : MonoBehaviour
{
    public Animator TransitionImage;

    // Start is called before the first frame update
    public void StartGame()
    {
        StartCoroutine(StartGameTransition());
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void Menu()
    {
        StartCoroutine(GoToMenu());
    }

    private IEnumerator StartGameTransition()
    {
        TransitionImage.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    private IEnumerator GoToMenu()
    {
        TransitionImage.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
