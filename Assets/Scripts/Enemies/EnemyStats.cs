using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public float maxHealth;
    public Spell attack;
    public SpellMod modifiers;
    public float sightRange;
    public float sightAngle;
    public float speed;
}
