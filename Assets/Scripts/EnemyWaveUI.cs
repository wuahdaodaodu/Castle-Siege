using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyWaveUI : MonoBehaviour
{
   [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;
    private RectTransform enemyWaveSpawnPositionIndicator;
    private RectTransform enemyClosePositionIndicator;
    private Camera mainCamera;

    private void Awake()
    {
        waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        enemyWaveSpawnPositionIndicator=transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        enemyClosePositionIndicator = transform.Find("enemyClosePositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        mainCamera = Camera.main;   
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChanged;
        SetWaveNumberText("Wave" + enemyWaveManager.GetWaveNum());
    }

    private void EnemyWaveManager_OnWaveNumberChanged(object sender, System.EventArgs e)
    {
        SetWaveNumberText("Wave" + enemyWaveManager.GetWaveNum());
    }

    private void Update()
    {
        HandleNextWaveMessage();

        HandleEnemyWavetPositionIndicator();



        HandleEnemyClosestPositionIndicator();



    }
 

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in" + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }
    private void HandleEnemyClosestPositionIndicator()
    {
        float targetMaxRadius = 9999f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(mainCamera.transform.position, targetMaxRadius);
        Enemy targetEnemy = null;
        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {

                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }
            }

        }
        if (targetEnemy != null)
        {
            Vector3 dirToCloseEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;
            enemyClosePositionIndicator.anchoredPosition = dirToCloseEnemy * 250f;
            enemyClosePositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngerFromVector(dirToCloseEnemy));

            float distanceToCloseEnemy = Vector3.Distance(targetEnemy.transform.position, mainCamera.transform.position);
            enemyClosePositionIndicator.gameObject.SetActive(distanceToCloseEnemy > mainCamera.orthographicSize * 1.5f);
        }
        else
        {
            enemyClosePositionIndicator.gameObject.SetActive(false);
        }

    }

    private void HandleEnemyWavetPositionIndicator()
    {
        Vector3 dirToNextSpawnPosition = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;
        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngerFromVector(dirToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.5f);




    }
    private  void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }
    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
}
