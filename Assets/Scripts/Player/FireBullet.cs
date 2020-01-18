using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    //TODO: Needs overhauling per bullet type - Instead call a "fire" function in the bullet?
    //Ice - constant velocity
    //Wind - initial thrust
    //Fire - No velocity - just particle effect
    //Electric - same as ice
    //Light - Hitscan, use a trail

    public GameObject bullet;

    public Weapon equipped;
    public float speed;
    public Camera cam;

    private PlayerControl pc;

    bool fire = false;

    private int cooldownFrames = 10;
    private int currCooldown;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponentInParent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currCooldown > 0) {
            currCooldown -= 1;
            return;
        }
        if(Input.GetButton("Fire1")) {
            fire = true;
            currCooldown = cooldownFrames;
        }
    }

    void FixedUpdate()
    {
        if(fire) {
            if(pc.isShmup()) {
                GameObject temp = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
                Rigidbody projectile = temp.GetComponent<Rigidbody>();
                projectile.velocity = transform.parent.forward * speed;
                fire = false;
            }
            else {
                Spell equippedSpell = equipped.spellPrefab;
                equippedSpell.fire(equipped.modifiers, transform, cam);
                fire = false;
            }
        }
    }
}
