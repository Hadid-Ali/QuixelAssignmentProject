using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilMethods : MonoBehaviour
{
    public static void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.MENUSCENE);
    }
}
