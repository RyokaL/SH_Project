using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpellCollision : MonoBehaviour, ISpellCollision
{
    private SpellMod modifiers;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == 0) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    // void Update() {
    //     timer += Time.deltaTime;
    //     Debug.Log(timer);
    //     if(timer >= modifiers.range) {
    //         GetComponent<Rigidbody>().velocity = Vector3.zero;
    //     }
    // }

    public void setModifiers(SpellMod modifiers) {
        this.modifiers = modifiers;
        Destroy(gameObject, modifiers.TTL);
    }
}
