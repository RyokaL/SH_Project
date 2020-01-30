using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Weapon : ScriptableObject
{ 
    public string weaponName;
    public GameObject staff;
    public GameObject cradle;
    public GameObject gem;

    public Spell spellPrefab;

    public SpellMod modifiers;

    public void set(string name, Spell spell, SpellMod mod) {
        this.weaponName = name;
        this.spellPrefab = spell;
        this.modifiers = mod;
    }
}