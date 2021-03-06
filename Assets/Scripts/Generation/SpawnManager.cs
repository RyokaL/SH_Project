﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public SpawnTable spawnTable;

    public GameObject particleEffect;
    public int maxEnemies = 1;

    public float spawnCooldown = 30;
    
    public float cooldownCounter = 0;

    private int enemiesSpawned = 0;
    private int totalSpawned = 0;
    public bool disableAfterSpawned = false;
    public int spawnBeforeDisable = 1;

    private List<GameObject> spawnedEnemies;

    private LevelStage lastSpawnStage = LevelStage.Starter;
    private List<SpawnTableDetails> completeSpawnTable;
    // Start is called before the first frame update
    void Start()
    {
        completeSpawnTable = new List<SpawnTableDetails>();
        spawnedEnemies = new List<GameObject>();
        if(spawnTable != null && spawnTable.StarterTable != null) {
            completeSpawnTable.AddRange(spawnTable.StarterTable);
        }
    }

    // Update is called once per frame

    private void cleanEnemyList() {
        spawnedEnemies.RemoveAll(x => x == null);
    }

    public GameObject spawnNewEnemy(LevelStage diff, float time) {
        if(diff != lastSpawnStage) {
            lastSpawnStage = diff;
            if(spawnTable != null) {
                completeSpawnTable.AddRange(spawnTable.getTable(diff));
            }
        }

        if(cooldownCounter >= spawnCooldown && completeSpawnTable.Count > 0 && spawnedEnemies.Count < maxEnemies) {
            cooldownCounter = 0;

            int randIndex = Random.Range(0, completeSpawnTable.Count);
            if(diff == LevelStage.Medium) {
                randIndex = Random.Range(randIndex, completeSpawnTable.Count);
            }
            if(diff == LevelStage.VeryHard) {
                randIndex = Random.Range(randIndex, completeSpawnTable.Count);
            }

            SpawnTableDetails entry = completeSpawnTable[randIndex];
            GameObject newEnemy = Instantiate(entry.enemyType, transform.position, transform.rotation);
            Enemy enClass = newEnemy.GetComponent<Enemy>();
            enClass.stats = calcStats(entry.maxStats, diff, time);

            float hue = (360 * enClass.stats.maxHealth / entry.maxStats.maxHealth) + 60;
            if(hue > 360) {
                hue = 0;
            }
            float sat = enClass.stats.maxHealth == entry.maxStats.minHealth ? 0 : 100;

            if(newEnemy.name.StartsWith("GroundEnemy")) {
                newEnemy.GetComponentInChildren<Renderer>().materials[1].color = Color.HSVToRGB(hue / 360, sat / 100, 1);
            }
            else {
                newEnemy.GetComponentInChildren<Renderer>().material.color = Color.HSVToRGB(hue / 360, sat / 100, 1);
            }


            spawnedEnemies.Add(newEnemy);
            Instantiate(particleEffect, transform.position, transform.rotation);
            return newEnemy;
        }
        else {
            return null;
        }
    }

    public static EnemyStats calcStats(EnemyMaxStats max, LevelStage diff, float time) {
        float health = max.minHealth + (int) (time/30) * max.healthMod;
        if(health > max.maxHealth) {
            health = max.maxHealth;
        }

        float speed = Random.Range(max.minSpeed, max.minSpeed + (time / 20) * max.speedMod);
        if(speed > max.maxSpeed) {
            speed = max.maxSpeed;
        }

        Spell attack = null;
        if(max.attacks != null && max.attacks.Count > 0) {
            int attackType = Random.Range(0, max.attacks.Count);
            attack = max.attacks[attackType];
        }

        SpellAttr attr = max.attackModifiers;
        SpellMod modifiers = new SpellMod();
        modifiers.damage = Random.Range(attr.minDamage, attr.minDamage + attr.minDamage * time / 90);
        if(modifiers.damage > attr.maxDamage) {
            modifiers.damage = attr.maxDamage;
        }

        modifiers.damagePercent = 1;

        int dotChance = Random.Range(0, 101);
        bool dot = (15 * (int)diff) >= dotChance;
        if(dot) {
            modifiers.dot = true;
            modifiers.dotTick = Random.Range(attr.minDot, attr.minDot + attr.minDot * time / 180);
            if(modifiers.dotTick > attr.maxDot) {
                modifiers.dotTick = attr.maxDot;
            }
            modifiers.dotLength = Random.Range(attr.minFireRate, attr.maxFireRate);
        }

        modifiers.TTL = Random.Range(attr.minTTL, attr.maxTTL);
        modifiers.fireRate = Random.Range(attr.minFireRate, attr.minFireRate + attr.minFireRate * time / 180);
        if(modifiers.fireRate > attr.maxFireRate) {
            modifiers.fireRate = attr.maxFireRate;
        }

        //TODO: Do range for some enemies after adding wind weapon

        return new EnemyStats(health, attack, modifiers, max.sightRange, max.sightAngle, speed);
    }

    void Update()
    {
       cooldownCounter += Time.deltaTime;
    }
}
