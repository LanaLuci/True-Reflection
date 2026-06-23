using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour
{
    private UIDocument _doc;
    private Button startButton, quitButton;

    private void Start()
    {
        _doc = GetComponent<UIDocument>();
        startButton = _doc.rootVisualElement.Q("StartButton") as Button;
        startButton.RegisterCallback<ClickEvent>(StartGameClick);
        quitButton = _doc.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(QuitGameClick);
    }

    private void StartGameClick(ClickEvent evt)
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGameClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnDisable()
    {
        startButton.UnregisterCallback<ClickEvent>(StartGameClick);
        quitButton.UnregisterCallback<ClickEvent>(QuitGameClick);
    }
}
