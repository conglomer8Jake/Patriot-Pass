using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;
    public bool volumeDown;
    public bool volumeUp;
    void Update()
    {
        /*
        //Press key to Fade Out(Press K to activate Fade out animation)_later connect with victory/losing condition
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Fade to designated scene "Build Settings, levelIndex")
            FadeToLevel(0);
            //This code will trigger next scene in buildIndex
            FadeToNextLevel();
        }*/
    }
    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("Transition_Fade_OUT_ use this trigger to transit to a different scene");

        volumeDown = true;
        volumeUp = false;
        if(volumeDown == true)
        {
            AudioListener.volume--;
            if(AudioListener.volume <= 0)
            {
                AudioListener.volume = 0;
            }
        }
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);

        volumeDown = false;
        volumeUp = true;
        if (volumeUp == true)
        {
            AudioListener.volume++;
            if (AudioListener.volume >= PlayerPrefs.GetFloat("gameVolume"))
            {
                AudioListener.volume = PlayerPrefs.GetFloat("gameVolume");
            }
        }
    }
}