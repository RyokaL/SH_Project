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
            case 0:
                if(couldFire || timeCount >= fireRateCooldown) {
                    if(Input.GetButton("Fire1")) {
                        fire = true;
                        if(couldFire) {
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
                break;
            case 1:
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
            case 2:
                break;
            case 3:
                break;
        }
        if(weaponChargeUI != null) {
            weaponChargeUI.sizeDelta = new Vector2((fireEnergy / maxTTL) * 100, weaponChargeUI.sizeDelta.y);
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
