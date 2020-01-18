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
        if(modifiers.pierce) {
            if(!other.gameObject.tag.Equals("enemy")) {
                Destroy(gameObject);
            }
        }
        else {
            Destroy(gameObject);
        }

        GameObject collidedObject = other.gameObject;
        if(collidedObject.tag.Equals("enemy")) {
            //Deal damage

            if(modifiers.dot) {
                //Also apply damage over time
            }
        }
    }
    void OnCollisionEnter(Collision collision) {
        if(modifiers.bounce) {
            //Do nothing, allow physics to happen
        }
        else {
            Destroy(gameObject);
        }

        //Apply damage
        GameObject collidedObject = collision.gameObject;
        if(collidedObject.tag.Equals("enemy")) {
            //Deal damage

            if(modifiers.dot) {
                //Also apply damage over time
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
