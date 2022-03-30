using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealth : MonoBehaviour
{
    public static float health;
    public float bossHeal;
    float heal1;
    int CurrentStage;
    int stage;
    int boss1TotalStage = 4;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        health = bossHeal;
        CurrentStage = bossMovement.bossStage;
        heal1 = health - (bossHeal * 0.1f);
        count = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(heal1);
        // health -= Time.deltaTime;
      //  Debug.Log("Health " + health);
        //if current health is less than 10% of intital health then stage will be change
        if(health <= heal1){
            count++;
            StageSelection();
        }
        heal1 = health - (bossHeal * 0.1f);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();

        if (damageDealer) //If player collides with anything that has damageDealer on it like enemy, laser or damage capsule
        {
            ProcessHit(damageDealer);
        }
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
            //decrease health
            health = health - damageDealer.GetDamage();

            if (health <= 0)
            {
                Die();
            }
        damageDealer.Hit();
    }

    private void Die()
    {
        Debug.Log("Boss1 dead");
    }

    void StageSelection(){

        if (count <= boss1TotalStage)
        {
            stage = (CurrentStage + 1);
        }
        else if(count > boss1TotalStage)
        {
            while (stage == CurrentStage)
            {
                stage = Random.Range(1, boss1TotalStage + 1);
            }
        }
        Debug.Log("Stage" + stage);
        bossMovement.bossStage = stage;
        CurrentStage = stage;
    }
}
