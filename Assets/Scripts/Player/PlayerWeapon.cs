using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    //Ice - constant velocity
    //Wind - initial thrust
    //Fire - No velocity - just particle effect
    //Electric - same as ice
    //Light - Hitscan, use a trail

    [SerializeField]
    public Weapon equipped;
    public float speed;
    public Camera cam;

    private PlayerControl pc;

    bool fire = false;

    private float timeCount = 0;
    private bool couldFire = false;
    private float fireRateCooldown;
    public float fireEnergy = 0;
    public bool switched = false;
    public float maxTTL = 0;

    private bool chargeHeld = false;
    public float chargeTime = 0;

    public RectTransform FireRateUI;
    public RectTransform weaponEnergyUI;
    public RectTransform weaponChargeUI;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInParent<PlayerControl>();
        fireRateCooldown = 1 / equipped.modifiers.fireRate;
        maxTTL = equipped.modifiers.TTL;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        switch(equipped.spellPrefab.spellAttributes.spellType) {
            //Normal Weapon with cooldown
            case 0:
                FireRateUI.parent.gameObject.SetActive(true);
                weaponChargeUI.parent.gameObject.SetActive(false);
                weaponEnergyUI.parent.gameObject.SetActive(false);

                if(couldFire || timeCount >= fireRateCooldown) {
                    if(Input.GetButton("Fire1")) {
                        fire = true;
                        if(couldFire) {
                            FireRateUI.sizeDelta = new Vector2(100, FireRateUI.sizeDelta.y);
                            timeCount = 0;
                            couldFire = false;
                        }
                    }
                    else if(!couldFire) {
                        couldFire = true;
                    }
                    if(timeCount >= fireRateCooldown) {
                        timeCount -= fireRateCooldown;
                    }
                }
                else {
                    FireRateUI.sizeDelta = new Vector2((timeCount / fireRateCooldown) * 100, FireRateUI.sizeDelta.y);
                }
                break;
            //Weapon with 'ammo'
            case 1:
                FireRateUI.parent.gameObject.SetActive(false);
                weaponChargeUI.parent.gameObject.SetActive(false);
                weaponEnergyUI.parent.gameObject.SetActive(true);
                if(switched) {
                    fireEnergy -= Time.deltaTime;
                    if(!Input.GetButton("Fire1")) {
                        if(!fire) {
                            switched = false;
                            equipped.spellPrefab.fire(equipped.modifiers, transform, cam);  
                        }
                    }
                    else if(fireEnergy <= 0) {
                        switched = false;
                        equipped.spellPrefab.fire(equipped.modifiers, transform, cam);  
                    }
                }
                else {
                    fireEnergy += Time.deltaTime;
                    if(fireEnergy >= maxTTL) {
                        fireEnergy = maxTTL;
                    }
                    if(Input.GetButton("Fire1")) {
                        if(fireEnergy > 0) {
                            fire = true;
                            switched = true;
                        }
                    }
                }
                break;
            //Charged Weapon
            case 2:
                FireRateUI.parent.gameObject.SetActive(false);
                weaponChargeUI.parent.gameObject.SetActive(true);
                weaponEnergyUI.parent.gameObject.SetActive(false);
                float fireTime = 1 / equipped.modifiers.fireRate;
                if(Input.GetButton("Fire1")) {
                    chargeHeld = true;
                    chargeTime += Time.deltaTime;
                    if(chargeTime > fireTime) {
                        chargeTime = fireTime;
                    }
                }
                else if(chargeHeld) {
                    chargeHeld = false;
                    equipped.modifiers.damagePercent = (chargeTime / fireTime);
                    if(chargeTime / fireTime == 1) {
                        equipped.modifiers.pierce = true;
                    }
                    else {
                        equipped.modifiers.pierce = false;
                    }
                    fire = true;
                    chargeTime = 0;
                }
                break;
            case 3:
                break;
        }
        if(weaponEnergyUI != null) {
            weaponEnergyUI.sizeDelta = new Vector2((fireEnergy / maxTTL) * 100, weaponEnergyUI.sizeDelta.y);
        }
        if(weaponChargeUI != null) {
            weaponChargeUI.sizeDelta = new Vector2((chargeTime / (1 / equipped.modifiers.fireRate)) * 100, weaponChargeUI.sizeDelta.y);
        }
    }

    void FixedUpdate()
    {
        if(fire) {
            if(pc.isShmup()) {
                //Not implemented
            }
            else {
                Spell equippedSpell = equipped.spellPrefab;
                equippedSpell.fire(equipped.modifiers, transform, cam);
                fire = false;
            }
        }
    }

    public void equipWeapon(Weapon newWeapon) {
        equipped = newWeapon;
        fireRateCooldown = 1 / equipped.modifiers.fireRate;
        maxTTL = equipped.modifiers.TTL;
    }
}
