using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour, ISpellCollision
{
    private float timeCount = 0;
    private float totalTimeOn = 0;
    private float damageCooldown = 0;
    private SpellMod modifiers;
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
            timeCount += Time.deltaTime;
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

    }

    public void setModifiers(SpellMod modifiers) {
        this.modifiers = modifiers;
        damageCooldown = 1 / modifiers.fireRate;
    }
}
