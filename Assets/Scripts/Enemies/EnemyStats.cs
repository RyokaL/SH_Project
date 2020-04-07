using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyStats
{
    public float maxHealth;
    public Spell attack;
    public SpellMod modifiers;
    public float sightRange;
    public float sightAngle;
    public float speed;

    public EnemyStats(float maxHealth, Spell attack, SpellMod mod, float sightRange, float sightAngle, float speed) {
        this.maxHealth = maxHealth;
        this.attack = attack;
        this.modifiers = mod;
        this.sightAngle = sightAngle;
        this.sightRange = sightRange;
        this.speed = speed;
    }
}
