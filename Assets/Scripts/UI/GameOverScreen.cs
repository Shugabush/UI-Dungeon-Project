using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Canvas screen;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    static Canvas Screen => instance.screen;

    static GameOverScreen instance;

    void Awake()
    {
        instance = this;

        restartButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        quitButton.onClick.AddListener(Application.Quit);
    }

    /// <summary>
    /// Trigger game over
    /// </summary>
    public static void Trigger()
    {
        ActiveCanvasManager.SetCanvasActive(Screen);
    }
}
