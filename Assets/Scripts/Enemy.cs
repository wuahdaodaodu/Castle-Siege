using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform targetTransform;
    private Rigidbody2D rigidbody2D;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax=0.2f;
    private HealthSystem healthSystem;

    public static Enemy Create(Vector3 position)
    {
      Transform pfEmeny=  Resources.Load<Transform>("pfEnemy");
      Transform enemyTransform=  Instantiate(pfEmeny,position,Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;    
    }
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (BuildingManager.Instance.GetHQBuilding()!=null)
        {
            targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
  
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDied += HealthSystem_OnDied;
        healthSystem.OnDamaged += HealthSystem_OnDamaged;
        lookForTargetTimer = Random.Range(0f, lookForTargetTimerMax);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
        CineMathineShake.Instance.ShakeCamera(5f, 0.1f);
        ColorEffect.Instance.SetWeight(0.5f);
    }

    private void HealthSystem_OnDied(object sender, System.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        CineMathineShake.Instance.ShakeCamera(7f, 0.15f);
        Instantiate(Resources.Load<Transform>("pfEnemyDieParticles"),transform.position, Quaternion.identity);
        ColorEffect.Instance.SetWeight(0.5f);
        Destroy(gameObject);
    }

    private void Update()
    {


        HandleMovement();
        Handletargeting();


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building=collision.gameObject.GetComponent<Building>();
        if (building!=null)
        {
           HealthSystem healthSystem= building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            this.healthSystem.Damage(9999);
        }
    }
    private void HandleMovement()
    {
        if (targetTransform != null)
        {
            //获取目标移动方向
            Vector3 moveDir = (targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;
            rigidbody2D.velocity = moveDir * moveSpeed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }
   private void Handletargeting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTargets();
        }

    }
    private void LookForTargets()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null) {
                if (targetTransform == null)
                {
                    targetTransform=building.transform;
                }
                else
                {
                    
                    if (Vector3.Distance(transform.position,building.transform.position)<Vector3.Distance(transform.position, targetTransform.position))
                    {
                        targetTransform = building.transform;
                    }
                }
            }
                
        }
        //设置默认目标
        if (targetTransform==null)
        {
            if (BuildingManager.Instance.GetHQBuilding()!=null)
            {
                targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
           
        }
    }
}
