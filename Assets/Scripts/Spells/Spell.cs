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
    public abstract void fire(SpellMod modifiers, Vector3 firePoint, Vector3 target);

    public abstract GameObject getProjectile();
    public abstract SpellAttr GetSpellAttr();
}