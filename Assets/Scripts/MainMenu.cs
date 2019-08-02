using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MainMenuState
{
    MainMenu,
    ExtrudeVariation,
    Loading
}

public class MainMenu : MonoBehaviour
{
    public MainMenuState mainMenuState;

    public GameObject mainScreen,extrudeVariation,loadingScreen;
    public Text loadingText;

    public float animationWait;

    private Coroutine routine;

    void LoadScene(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void Assignment1()
    {
        this.ChangeMainMenuState(MainMenuState.Loading);
        this.LoadScene(Constants.SCENE1);
    }

    public void Assignment2()
    {
        this.ChangeMainMenuState(MainMenuState.Loading);
        this.LoadScene(Constants.SCENE2);
    }

    public void Assignment3()
    {
        this.ChangeMainMenuState(MainMenuState.ExtrudeVariation);
    }

    public void Assignment3Var1()
    {
        this.ChangeMainMenuState(MainMenuState.Loading);
        this.LoadScene(Constants.SCENE3_1);
    }

    public void Assignment3Var2()
    {
        this.ChangeMainMenuState(MainMenuState.Loading);
        this.LoadScene(Constants.SCENE3_2);
    }

    public void Back()
    {
        this.ChangeMainMenuState(MainMenuState.MainMenu);
    }

    public void ChangeMainMenuState(MainMenuState mainMenuState)
    {
        this.mainMenuState = mainMenuState;


        this.extrudeVariation.SetActive(mainMenuState.Equals(MainMenuState.ExtrudeVariation));
        this.loadingScreen.SetActive(mainMenuState.Equals(MainMenuState.Loading));
        this.mainScreen.SetActive(mainMenuState.Equals(MainMenuState.MainMenu));

        if (mainMenuState.Equals(MainMenuState.Loading))
        {
            StartCoroutine(this.TextAnimation());
        }

        else
        {
            if (this.routine != null)
                StopCoroutine(this.routine);
        }
    }


    public IEnumerator TextAnimation()
    {
        while(true)
        {
            this.loadingText.text = ".";
            yield return new WaitForSeconds(this.animationWait);
            this.loadingText.text = "..";
            yield return new WaitForSeconds(this.animationWait);
            this.loadingText.text = "...";
            yield return new WaitForSeconds(this.animationWait);
            this.loadingText.text = "....";
            yield return new WaitForSeconds(this.animationWait);
        }
    }
}
