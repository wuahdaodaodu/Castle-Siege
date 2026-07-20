using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private MusicManager musicManager;
    private TextMeshProUGUI soundVolumeText;
    private TextMeshProUGUI musicVolumeText;

    private void Awake()
    {
        soundVolumeText = transform.Find("soundVolumeText").GetComponent<TextMeshProUGUI>();
        musicVolumeText = transform.Find("musicVolumeText").GetComponent<TextMeshProUGUI>();
        transform.Find("SoundIncreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.IncreaseVolume();
            UpdateText();



        });
        transform.Find("SoundDecreaseBtn").GetComponent<Button>().onClick.AddListener(() => {
            soundManager.DecreaseVolume();
            UpdateText();

        });
        transform.Find("Music+").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.IncreaseVolume();
            UpdateText();

        });
        transform.Find("Music-").GetComponent<Button>().onClick.AddListener(() => {
            musicManager.DecreaseVolume();
            UpdateText();

        });
        transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() => {
            Time.timeScale = 1f;
            GameSenceManager.Load(GameSenceManager.Scene.MainMenuScene);
   

         

        }
        
        );
        transform.Find("edgeScrollingToggle").GetComponent<Toggle>().onValueChanged.AddListener((bool set) =>
        {
            CameraHandler.Instance.SetedgeScrolling(set);
        });

    }
    private void Start()
    {
        UpdateText();
        gameObject.SetActive(false);
        transform.Find("edgeScrollingToggle").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetEdgeScrolling());
    }
    private void UpdateText()
    {
        soundVolumeText.SetText(Mathf.RoundToInt( soundManager.GetVolume()*10).ToString());
       musicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 10).ToString());
    }
    public void ToggleVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
  


}
