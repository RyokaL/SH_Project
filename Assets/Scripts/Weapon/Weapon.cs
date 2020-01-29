using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weapon : ScriptableObject
{ 
    public string weaponName;
    public GameObject staff;
    public GameObject cradle;
    public GameObject gem;

    public Spell spellPrefab;

    public SpellMod modifiers;

}