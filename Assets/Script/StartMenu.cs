using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour
{
    private UIDocument _doc;
    private Button startButton, quitButton, settingsButton, settingsCloseButton, instructionButton, instructionCloseButton;
    private VisualElement settingsContainer, instructionContainer;
    private Slider bgmSlider, sfxSlider;
    private List<Button> _menuButtons = new List<Button>();
    public UnityEvent OnAnyButtonClicked;
    

    private void Start()
    {
        _doc = GetComponent<UIDocument>();
        _menuButtons =  _doc.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].RegisterCallback<ClickEvent>(OnAnyClick);
        }
        //for start and quit
        startButton = _doc.rootVisualElement.Q("StartButton") as Button;
        startButton.RegisterCallback<ClickEvent>(StartGameClick);
        quitButton = _doc.rootVisualElement.Q("QuitButton") as Button;
        quitButton.RegisterCallback<ClickEvent>(QuitGameClick);
        //for settings window
        settingsButton = _doc.rootVisualElement.Q("SettingsButton") as Button;
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsOpen);
        settingsCloseButton = _doc.rootVisualElement.Q("SettingsCloseButton") as Button;
        settingsCloseButton.RegisterCallback<ClickEvent>(OnSettingsExit);
        settingsContainer = _doc.rootVisualElement.Q("Settings");
        settingsContainer.style.display = DisplayStyle.None;
        //for instruction window
        instructionButton = _doc.rootVisualElement.Q("InstructionButton") as Button;
        instructionButton.RegisterCallback<ClickEvent>(OnInstructionOpen);
        instructionCloseButton = _doc.rootVisualElement.Q("InstructionCloseButton") as Button;
        instructionCloseButton.RegisterCallback<ClickEvent>(OnInstructionExit);
        instructionContainer = _doc.rootVisualElement.Q("Instruction");
        instructionContainer.style.display = DisplayStyle.None;
        //for setting sliders
        bgmSlider = _doc.rootVisualElement.Q("BGMSlider") as Slider;
        bgmSlider.RegisterValueChangedCallback(OnBGMChanged);
        sfxSlider = _doc.rootVisualElement.Q("SFXSlider") as Slider;
        sfxSlider.RegisterValueChangedCallback(OnSFXChanged);

    }

    private void StartGameClick(ClickEvent evt)
    {
        StartCoroutine(DelayedSceneSwitch());
    }

    private IEnumerator DelayedSceneSwitch()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("SampleScene");
    }

    private void QuitGameClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void OnSettingsOpen(ClickEvent evt)
    {
        settingsContainer.style.display = DisplayStyle.Flex;
    }
    private void OnSettingsExit(ClickEvent evt)
    {
        settingsContainer.style.display = DisplayStyle.None;
    }

    private void OnInstructionOpen(ClickEvent evt)
    {
        instructionContainer.style.display = DisplayStyle.Flex;
    }
    private void OnInstructionExit(ClickEvent evt)
    {
        instructionContainer.style.display = DisplayStyle.None;
    }

    private void OnAnyClick(ClickEvent evt)
    {
        OnAnyButtonClicked.Invoke();
    }

    private void OnBGMChanged(ChangeEvent<float> evt)
    {
        AudioManager.instance.SetBGM(evt.newValue);
        PlayerPrefs.SetFloat("BGMVolume", evt.newValue);
        PlayerPrefs.Save();
    }
    private void OnSFXChanged(ChangeEvent<float> evt)
    {
        AudioManager.instance.SetSFX(evt.newValue);
        PlayerPrefs.SetFloat("SFXVolume", evt.newValue);
        PlayerPrefs.Save();
    }

    private void OnDisable()
    {
        for (int i = 0; i < _menuButtons.Count; i++)
        {
            _menuButtons[i].UnregisterCallback<ClickEvent>(OnAnyClick);
        }
        startButton.UnregisterCallback<ClickEvent>(StartGameClick);
        quitButton.UnregisterCallback<ClickEvent>(QuitGameClick);
        settingsButton.UnregisterCallback<ClickEvent>(OnSettingsOpen);
        settingsCloseButton.UnregisterCallback<ClickEvent>(OnSettingsExit);
        bgmSlider.UnregisterValueChangedCallback(OnBGMChanged);
        sfxSlider.UnregisterValueChangedCallback(OnSFXChanged);
    }
}
