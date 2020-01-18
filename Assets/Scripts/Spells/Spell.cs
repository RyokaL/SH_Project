using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Spell : ScriptableObject
{
    [SerializeField]
    public GameObject projectile;
    [SerializeField]
    public SpellAttr spellAttributes;
    public abstract void fire(SpellMod modifiers, Transform firePoint, Camera mainCam);

    public abstract GameObject getProjectile();
    public abstract SpellAttr GetSpellAttr();
}