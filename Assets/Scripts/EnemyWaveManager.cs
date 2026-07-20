using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public event EventHandler OnWaveNumberChanged;
    public static EnemyWaveManager Instance {  get; private set; }





    [SerializeField] private List<Transform>  spawnPositionTransformList;
    [SerializeField] private Transform nextSpawnPositionTransform;
    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }
    private State state;
    private int waveNum;
    private float nextWaveSpawnTimer;
    private float nextEnemyWaveSpawnTimer;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;
    private void Awake()
    {
            Instance = this;
    }
    private void Start()
    {
        spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        state = State.WaitingToSpawnNextWave;
        nextSpawnPositionTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;
                if (nextWaveSpawnTimer < 0)
                {
                    SpawnWave();
                    SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyWaveStarting);
                }
                break;

            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextEnemyWaveSpawnTimer -= Time.deltaTime;
                    if (nextEnemyWaveSpawnTimer < 0)
                    {
                        nextEnemyWaveSpawnTimer = UnityEngine.Random.Range(0, 0.2f);
                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if (remainingEnemySpawnAmount<=0)
                        {
                            state=State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextSpawnPositionTransform.position = spawnPosition;
                            nextWaveSpawnTimer = 15f;
                        }
                    }
                    
                 }
                break;





        }
        
        
    }
    private void SpawnWave()
    {    
      
        remainingEnemySpawnAmount = 5+3*waveNum;
        state = State.SpawningWave;
        waveNum++;
        OnWaveNumberChanged?.Invoke(this,EventArgs.Empty);
    }
    public int GetWaveNum()
    {
        return waveNum;
    }
    public float GetNextWaveSpawnTimer()
    {
        return nextWaveSpawnTimer;
    }
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
