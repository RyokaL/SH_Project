using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellAttr 
{
    
    public float minDamage = 0;
    public float maxDamage = 0;
    public float minTTL = 0;
    public float maxTTL = 0;
    public float minFireRate = 0;
    public float maxFireRate = 0;
    public float minRange = 0;
    public float maxRange = 0;
    public float minDot = 0;
    public float maxDot = 0;
    public SpellAttr(float minDamage, float maxDamage, float minTTL, float maxTTL, float minFireRate, float maxFireRate, 
                    float minRange, float maxRange, float minDot, float maxDot) {
        
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        this.minTTL = minTTL;
        this.maxTTL = maxTTL;
        this.minFireRate = minFireRate;
        this.maxFireRate = maxFireRate;
        this.minRange = minRange;
        this.maxRange = maxRange;
        this.minDot = minDot;
        this.maxDot = maxDot;
    }
}