using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSwarmLeaderAI : AI
{
    public List<FlyingSwarmAI> boids;
    private int nBoids;
    private Vector3 sumOfAllBoids;

    private bool charge = false;
    private float chargeCooldown;
    private float CHARGE_COOL = 10;

    private bool stuck = false;

    private Vector3 lastChargeVelocity = Vector3.zero;

    public override void nextUpdate(GameObject avatar, EnemyStats stats) {
        boids.RemoveAll(f => f == null);
        nBoids = boids.Count;
        sumOfAllBoids = Vector3.zero;

        foreach(FlyingSwarmAI b in boids) {
            Rigidbody bR = b.GetComponent<Rigidbody>();
            sumOfAllBoids += bR.position;
        }

        Rigidbody leader = avatar.GetComponent<Rigidbody>();

        if(!stuck) {
            Vector3 r1 = Vector3.zero;
            Collider[] playerInRange = Physics.OverlapSphere(avatar.transform.position, stats.sightRange);
            foreach(Collider c in playerInRange) {
                if(c.gameObject.tag == "Player") {
                    if(Vector3.Angle(avatar.transform.position, c.transform.position) < stats.sightAngle) {
                        if(chargeCooldown > CHARGE_COOL && Mathf.Abs((c.transform.position - leader.position).magnitude) < 20) {
                            if(!charge) {
                                leader.velocity = (c.transform.position - leader.position) * 2;
                                lastChargeVelocity = leader.velocity;
                                charge = true;
                            }
                            chargeCooldown -= CHARGE_COOL;
                        }
                        else {
                            chargeCooldown += Time.deltaTime;
                            r1 = (c.transform.position - leader.position) / 100;
                        }
                    }
                }
            }

            if(!charge) {
                Vector3 centerOfOtherBoids = (sumOfAllBoids) / (nBoids);
                Vector3 v1 = (centerOfOtherBoids - leader.position) / 100;
                Vector3 r2 = Vector3.zero;
                Vector3 r3 = Vector3.zero;
                foreach(FlyingSwarmAI d in boids) {
                    GameObject otherB = d.gameObject;
                    Rigidbody otherBR = otherB.GetComponent<Rigidbody>();
                        //Rule 3: Match velocity of nearby boids
                        r3 = r3 + otherBR.velocity;
                }
                Collider[] otherObjects = Physics.OverlapSphere(leader.position, 5);
                foreach(Collider c in otherObjects) {
                    if(c.GetComponent<FlyingSwarmAI>() || c.gameObject.tag.Equals("PlayerAttack")) {
                        continue;
                    }
                    GameObject otherB = c.gameObject;
                    r2 = r2 - (otherB.transform.position - leader.position);
                }

                Vector3 v3 = (r3 / (nBoids - 1) - leader.velocity) / 32;
                leader.velocity = leader.velocity + v1 + r1 + r2 + v3;
            }
        }
        avatar.transform.LookAt(leader.position + leader.velocity);
        calculateBoids(avatar, stats);
    }

    void OnTriggerEnter(Collider other) {
        if(charge) {
            charge = false;
            Rigidbody thisRigid = GetComponent<Rigidbody>();
            if(other.gameObject.tag.Equals("Player")) {
                thisRigid.velocity = new Vector3(-lastChargeVelocity.x / 4, -lastChargeVelocity.y / 8, -lastChargeVelocity.z / 4);
            }
            else {
                //Stuck in object
                thisRigid.velocity = Vector3.zero;
                stuck = true;
            }
        }
        //Apply damage and cooldown;
    }

    void calculateBoids(GameObject avatar, EnemyStats stats) {
        Rigidbody leader = avatar.GetComponent<Rigidbody>();

        Vector3 v1, v2, v3;

        foreach(FlyingSwarmAI b in boids) {
            Rigidbody boidRigid = b.GetComponent<Rigidbody>();
            GameObject boid = b.gameObject;

            //Rule 1: Boids always move towards the centre of mass of the other boids
            Vector3 centerOfOtherBoids = (sumOfAllBoids - boidRigid.position) / (nBoids - 1);
            v1 = (centerOfOtherBoids - b.transform.position) / 100;
            
            Vector3 r2 = Vector3.zero;
            Vector3 r3 = Vector3.zero;
            foreach(FlyingSwarmAI d in boids) {
                GameObject otherB = d.gameObject;
                Rigidbody otherBR = otherB.GetComponent<Rigidbody>();
                if(otherB != b) {
                    //Rule 2: Boids keep away from other boids and objects
                    if((otherBR.position - boidRigid.position).magnitude < 10) {
                        r2 = r2 - (otherBR.position - boidRigid.position);
                    }
                    //Rule 3: Match velocity of nearby boids
                    r3 = r3 + otherBR.velocity;
                }
            }

            Collider[] otherObjects = Physics.OverlapSphere(b.transform.position, 5);
            foreach(Collider c in otherObjects) {
                if(c.GetComponent<FlyingSwarmAI>()) {
                    continue;
                }
                GameObject otherB = c.gameObject;
                r2 = r2 - (otherB.transform.position - b.transform.position);
            }

            v2 = r2;
            v3 = (r3 / (nBoids - 1) - boidRigid.velocity) / 8;
            Vector3 v4 = (leader.position - boidRigid.position) / 100;

            boidRigid.velocity = boidRigid.velocity + (v1 + v2 + v3 + v4);
            if(boidRigid.velocity.magnitude > stats.speed) {
                boidRigid.velocity = boidRigid.velocity.normalized * stats.speed;
            }
            boid.transform.LookAt(boidRigid.position + boidRigid.velocity);
        }
    }

    public override void onDeath(GameObject root) {
        //Chance to assign new leader or turn swarm into panic mode
        Destroy(root);
    }
}
