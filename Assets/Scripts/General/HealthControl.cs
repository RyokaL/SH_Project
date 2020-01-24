using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControl : MonoBehaviour {

    public float currHealth = 0;
    public RectTransform healthBarImg = null;
    private bool hasDot = false;
    private float dotDamage = 0;
    private float dotTicks = 0;
    private float dotTimer = 0;
    private float maxHealth = 0;

    public float getHealth() {
        return currHealth;
    }

    public void setMaxHealth(float max) {
        this.maxHealth = max;
        this.currHealth = max;
    }

    public void takeDamage(float damage) {
        currHealth -= damage;
        updateHealthBar();
    }

    public void applyDot(float damage, float ticks) {
        hasDot = true;
        dotDamage = damage;
        dotTicks = ticks;
    }

    private void updateHealthBar() {
        if(healthBarImg != null) {
            healthBarImg.sizeDelta = new Vector2((currHealth / maxHealth) * 200, healthBarImg.sizeDelta.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Handle if dot has been applied
        if(hasDot) {
            if(dotTicks > 0) {
                dotTimer += Time.deltaTime;
                if(dotTimer > 1) {
                    dotTicks -= 1;
                    currHealth -= dotDamage;
                    dotTimer -= 1;
                    updateHealthBar();
                }
            }
            else {
                hasDot = false;
                dotTimer = 0;
                dotDamage = 0;
                dotTicks = 0;
            }
        }
    }
}