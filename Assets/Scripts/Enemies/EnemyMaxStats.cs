using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyMaxStats : ScriptableObject {
    public float minHealth;
    //How max health changes over time
    public float healthMod;

    public float maxHealth;
    public SpellAttr attackModifiers;
    public float sightRange;
    public float sightAngle;

    public List<Spell> attacks;

    public float minSpeed;
    public float speedMod;

    public float maxSpeed;
}