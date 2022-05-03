using UnityEngine;
using UnityEngine.SceneManagement;

public class TEST_SceneTransition : MonoBehaviour
{

    public Animator animator;
    public string LoadScene = "";


    // Update is called once per frame
    void Update()
    {
        //Press key to Fade Out(Press K to activate Fade out animation)_later connect with victory/losing condition
        if (Input.GetKeyDown(KeyCode.K))
        {

            //Fade to designated scene "Build Settings, levelIndex")
            //FadeToLevel (0);
            SceneManager.LoadScene(LoadScene);


            /*
            //This code will trigger next scene in buildIndex
            FadeToNextLevel();
            */

        }

    }
    /*
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */

    public void FadeToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        animator.SetTrigger("Transition_Fade_OUT_ use this trigger to transit to a different scene");

    }
    /*
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Transition_Fade_OUT_ use this trigger to transit to a different scene");

    }
    */


    public void OnFadeComplete()
    {
        SceneManager.LoadScene(LoadScene);
    }
}
