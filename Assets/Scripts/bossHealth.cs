using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealth : MonoBehaviour
{
    public static float health;
    public float bossHeal;
    float heal1;
    int CurrentStage;
    int stage = 1;
    // Start is called before the first frame update
    void Start()
    {
        health = bossHeal;
        CurrentStage = bossMovement.bossStage;
        heal1 = health - (bossHeal * 0.1f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        health -= Time.deltaTime;
        //if current health is less than 10% of intital health then stage will be change
        if(health <= heal1){
            StageSelection();
            heal1 = health - (bossHeal * 0.2f);
        }
        
    }

    void StageSelection(){
        while(stage == CurrentStage){
            stage = Random.Range(1,4);
        }
        Debug.Log("Stage" + stage);
        bossMovement.bossStage = stage;
        CurrentStage = stage;
    }
}
