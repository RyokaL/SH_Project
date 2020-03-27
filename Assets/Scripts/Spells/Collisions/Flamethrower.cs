using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, ISpellCollision
{
    private float timeCount = 0;
    private bool calcDamage = false;
    private float damageCooldown = 0;
    private SpellMod modifiers;

    private float timeSinceStart = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(modifiers.dot) {
            if(other.gameObject.tag.Equals("Enemy")) {
                HealthControl collidedHealth = other.gameObject.GetComponent<HealthControl>();
                collidedHealth.applyDot(modifiers.dotTick, modifiers.dotLength);
            }
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.gameObject.tag.Equals("Enemy")) {
            if(!calcDamage) {
                timeCount += Time.deltaTime;
                calcDamage = true;
            }
            if(timeCount >= damageCooldown) {
                HealthControl collidedHealth = other.gameObject.GetComponent<HealthControl>();
                collidedHealth.takeDamage(modifiers.damage);

                timeCount -= Time.deltaTime;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        calcDamage = false;
        timeSinceStart += Time.deltaTime;
        float maxTime = 1 / modifiers.TTL;
        if(timeSinceStart >= maxTime) {
            timeSinceStart = maxTime;
        }
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, timeSinceStart / maxTime);
    }

    public void setModifiers(SpellMod modifiers) {
        this.modifiers = modifiers;
        damageCooldown = 1 / modifiers.fireRate;
    }
}
