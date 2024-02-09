using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Canvas screen;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    static Canvas Screen => instance.screen;

    static GameOverScreen instance;

    [SerializeField] UnityEvent onWin = new UnityEvent();
    [SerializeField] UnityEvent onLose = new UnityEvent();

    void Awake()
    {
        instance = this;

        restartButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        quitButton.onClick.AddListener(Application.Quit);
    }

    /// <summary>
    /// Trigger win
    /// </summary>
    public static void Win()
    {
        instance.onWin?.Invoke();
        ActiveCanvasManager.SetCanvasActive(Screen);
        LowHealthIndicator.Disable();
    }

    /// <summary>
    /// Trigger lose
    /// </summary>
    public static void Lose()
    {
        instance.onLose?.Invoke();
        ActiveCanvasManager.SetCanvasActive(Screen);
        LowHealthIndicator.Disable();
    }
}
