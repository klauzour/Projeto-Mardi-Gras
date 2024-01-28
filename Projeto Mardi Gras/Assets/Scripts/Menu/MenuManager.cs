using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject optionsPanel;
    [SerializeField] Button optionsFirstButton;
    [SerializeField] GameObject mainPanel;
    [SerializeField] Button mainFirstButton;
    [SerializeField] Slider volumeSlider;

    List<int> widths = new List<int>() { 568, 960, 1280, 1920 };
    List<int> heights = new List<int>() { 329, 540, 800, 1080 };

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
    }

    public void Play()
    {
        // Trocar "play" pela cena certa 
        SceneManager.LoadScene("Play");
    }

    public void Config()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(optionsFirstButton.gameObject);
    }

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];
        Screen.SetResolution(width, height, fullscreen);
    }

    public void SetFullscreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void Return()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(mainFirstButton.gameObject);
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GGLink()
    {
        Application.OpenURL("https://globalgamejam.org/group/25/games");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
