using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Spells", menuName = "Spells/Fire")]
public class Fire : Spell {
    GameObject beam = null;
    bool switchedOn = false;
    public override void fire(SpellMod modifiers, Transform firePoint, Camera mainCam) {
        if(!switchedOn) {
            switchedOn = true;
            beam = Instantiate(projectile, firePoint.position, firePoint.rotation);
            beam.transform.forward = mainCam.transform.forward;
            beam.transform.parent = firePoint;
            beam.GetComponent<ISpellCollision>().setModifiers(modifiers);
            beam.GetComponent<ParticleSystem>().Play();
        }
        else {
            switchedOn = false;
            if(beam == null) {
                return;
            }
            beam.GetComponent<ParticleSystem>().Stop();
            Destroy(beam);
            return;
        }
        //Destroy(beam, modifiers.TTL);
    }

    public override void fire(SpellMod modifiers, Transform firePoint, Transform target) {
        return;
    }

    public override GameObject getProjectile() {
        return projectile;
    }
    public override SpellAttr GetSpellAttr() {
        return spellAttributes;
    }
}