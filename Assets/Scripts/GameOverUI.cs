using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        transform.Find("RetryBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSenceManager.Load(GameSenceManager.Scene.GameScene);
        
        });
        transform.Find("MainMenuBtn").GetComponent<Button>().onClick.AddListener(() => {

            GameSenceManager.Load(GameSenceManager.Scene.MainMenuScene);
        });
        Hide();
    }
   public  void Show()
    {
         gameObject.SetActive(true);
        transform.Find("waveSurvivedText").GetComponent<TextMeshProUGUI>().SetText("Your Survived" + EnemyWaveManager.Instance.GetWaveNum()+ "Waves!");
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
