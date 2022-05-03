using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class LoadScene : MonoBehaviour
{

    public string SceneToLoad = "__";

    //[SerializeField] GameObject levelSelect;
    //[SerializeField] GameObject mainMenuButtons;


    void Start()
    {
        //levelSelect.SetActive(false);
    }

    public void LoadLevel()
    {
        //FindObjectOfType<AudioManager>().Play("MenuConfirm");
        //StartCoroutine(waiter());
        SceneManager.LoadScene(SceneToLoad);

    }

    public void LoadLevel(string levelToLoad)
    {
        //FindObjectOfType<AudioManager>().Play("MenuConfirm");
        //StartCoroutine(waiter());
        SceneManager.LoadScene(levelToLoad);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    /*
    public void LevelSelect()
    {
        levelSelect.SetActive(true);
        mainMenuButtons.SetActive(false);
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LoadLevel();
    }
    /*
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(1f);
    }
    */
}