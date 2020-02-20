using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats;
    public HealthControl health;
    public Transform rootTransform;

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
            if(rootTransform != null) {
                Destroy(rootTransform.gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }
        ai.nextUpdate(rootTransform.gameObject, stats);
    }
}
