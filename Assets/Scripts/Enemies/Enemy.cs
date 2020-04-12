using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats;
    public HealthControl health;
    public Transform rootTransform;

    private bool dead;

    public AI ai;

    void Awake() {

    }

    // Start is called before the first frame update
    void Start()
    {
        health.setMaxHealth(stats.maxHealth);
        if(rootTransform == null) {
            rootTransform = transform;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(health.getHealth() <= 0) {
            if(!dead) {
                GameObject.Find("PlayerTest").GetComponent<OpenChest>().addPoints((int)stats.maxHealth);
                dead = true;
            }
            
            if(rootTransform != null) {
                ai.onDeath(rootTransform.gameObject);
            }
            else {
                ai.onDeath(gameObject);
            }
        }
    }

    void FixedUpdate() {
        ai.timeCheck(rootTransform.gameObject, stats);
    }
}
