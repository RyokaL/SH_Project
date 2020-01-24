using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public EnemyStats stats;
    public HealthControl health;
    public Transform rootTransform;

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

        Collider[] playerInRange = Physics.OverlapSphere(rootTransform.position, stats.sightRange);
        foreach(Collider c in playerInRange) {
            if(c.gameObject.tag == "Player") {
                if(Vector3.Angle(rootTransform.position, c.transform.position) < stats.sightAngle) {
                    transform.root.LookAt(c.transform);
                    //Enter chase mode? For now just keep moving if in range
                    rootTransform.position += rootTransform.forward * Time.deltaTime * stats.speed;
                    break;
                } 
            }
        }
    }
}
