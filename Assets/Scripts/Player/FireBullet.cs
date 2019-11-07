using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bullet;
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
                GameObject temp = Instantiate(bullet) as GameObject;
                temp.transform.position = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, cam.nearClipPlane));
                Rigidbody projectile = temp.GetComponent<Rigidbody>();
                projectile.velocity = cam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)).direction * speed;
                fire = false;
            }
        }
    }
}
