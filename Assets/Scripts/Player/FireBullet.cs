using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bullet;
    public float speed;
    public Transform cam;

    bool fire = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            fire = true;
        }
    }

    void FixedUpdate()
    {
        if(fire) {
            GameObject temp = Instantiate(bullet) as GameObject;
            temp.transform.position = transform.position + cam.forward;
            Rigidbody projectile = temp.GetComponent<Rigidbody>();
            projectile.velocity = cam.forward * speed;
            fire = false;
        }
    }
}
