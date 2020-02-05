using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCollider : MonoBehaviour {

    public TreasureGen weaponChoose;
    void OnTriggerEnter(Collider collision) {
        PlayerWeapon playerCollide = null;
        playerCollide = collision.gameObject.GetComponentInChildren<PlayerWeapon>();
        if(playerCollide != null) {
            playerCollide.equipWeapon(weaponChoose.getWeapon());
            Destroy(gameObject);
        }
    }
}