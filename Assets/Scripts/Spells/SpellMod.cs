using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellMod
{
    public float damage = 0;
    public bool dot = false;
    public float dotTick = 0;
    public bool pierce = false;
    public bool bounce = false;

    public bool track = false;
    public int numBullets = 1;
    public float fireRate = 1;
    public float TTL = 0;
    public float range = 0;
}