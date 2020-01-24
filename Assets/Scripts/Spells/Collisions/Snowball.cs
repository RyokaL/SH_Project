using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour, ISpellCollision
{
    private SpellMod modifiers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        enemyCollision(other.gameObject);

        if(modifiers.pierce) {
            if(!other.gameObject.tag.Equals("Enemy")) {
                Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision) {
        //Apply damage
        enemyCollision(collision.gameObject);
        

        if(modifiers.bounce) {
            //Do nothing, allow physics to happen
        }
        else {
            Destroy(gameObject);
        }
    }

    void enemyCollision(GameObject collidedObject) {
        if(collidedObject.tag.Equals("Enemy")) {
            HealthControl collidedHealth = collidedObject.GetComponent<HealthControl>();
            collidedHealth.takeDamage(modifiers.damage);

            if(modifiers.dot) {
                collidedHealth.applyDot(modifiers.dotTick, modifiers.dotLength);
            }
        }
    }

    public void setModifiers(SpellMod modifiers) {
        this.modifiers = modifiers;
        if(modifiers.bounce) {
            GetComponent<Collider>().isTrigger = false;
        }
        Destroy(gameObject, modifiers.TTL);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
