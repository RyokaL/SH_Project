using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCollider : MonoBehaviour {

    private Weapon currentGen = null;
    public SpellList spellTypes;
    private Weapon getWeapon() {
        Spell weaponSpell = spellTypes.spells[(int)Random.Range(0, spellTypes.spells.Count)];
        SpellAttr limits = weaponSpell.GetSpellAttr();

        SpellMod modifiers = new SpellMod();
        modifiers.damage = Random.Range(limits.minDamage, limits.maxDamage);
        //TODO: Change to scale
        modifiers.dot = Random.Range(0, 100) < 20;
        if(modifiers.dot) {
            modifiers.dotTick = Random.Range(limits.minDot, limits.maxDot);
            //TODO: Change to scale
            modifiers.dotLength = Random.Range(0, 5);
        }
        //TODO: Change to scale
        modifiers.bounce = Random.Range(0, 100) < 20;
        modifiers.pierce = Random.Range(0, 100) < 20;
        modifiers.track = Random.Range(0, 100) < 20;
        modifiers.numBullets = (int)Random.Range(0, 5);

        modifiers.TTL = Random.Range(limits.minTTL, limits.maxTTL);
        modifiers.range = Random.Range(limits.minRange, limits.maxRange);
        modifiers.fireRate = Random.Range(limits.minFireRate, limits.maxFireRate);

        Weapon newWeapon = (Weapon)ScriptableObject.CreateInstance("Weapon");
        newWeapon.set("Test Weapon", weaponSpell, modifiers);
        return newWeapon;
    }

    public void reRoll() {
        currentGen = null;
    }

    public Weapon getWeaponInfo() {
        if(currentGen == null) {
            currentGen = getWeapon();
        }
        return currentGen;
    }

    public void takeWeapon() {
        Destroy(gameObject);
    }

    // void OnTriggerEnter(Collider collision) {
    //     PlayerWeapon playerCollide = null;
    //     playerCollide = collision.gameObject.GetComponentInChildren<PlayerWeapon>();
    //     if(playerCollide != null) {
    //         playerCollide.equipWeapon(weaponChoose.getWeapon());
    //         Destroy(gameObject);
    //     }
    // }
}