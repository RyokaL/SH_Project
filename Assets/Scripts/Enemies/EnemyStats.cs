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
}
