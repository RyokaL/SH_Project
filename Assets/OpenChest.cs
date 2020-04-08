using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private bool wantToOpen = false;
    private bool open = false;

    private bool close = false;

    private ChestCollider lastChestOpen = null;

    public GameObject chestGUI;

    public Camera playerCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit")) {
            wantToOpen = true;
        }
        if(Input.GetButtonDown("Cancel")) {
            if(open) {
                close = true;
            }
        }
    }

    void FixedUpdate() {
        RaycastHit check;
        if(Physics.SphereCast(playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCam.nearClipPlane)), 1, playerCam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)).direction, out check, 5)) {
            if(!check.collider.gameObject.GetComponent<ChestCollider>()) {
                chestGUI.SetActive(false);
                close = false;
                open = false;
                wantToOpen = false;
                lastChestOpen = null;
                Time.timeScale = 1f;
            }
        }
        if(close) {
            chestGUI.SetActive(false);
            close = false;
            open = false;
            wantToOpen = false;
            lastChestOpen = null;
            Time.timeScale = 1f;
        }
        if(open && wantToOpen) {
            gameObject.GetComponentInChildren<PlayerWeapon>().equipWeapon(lastChestOpen.getWeaponInfo());
            lastChestOpen.takeWeapon();
            open = false;
            wantToOpen = false;
            lastChestOpen = null;
            Time.timeScale = 1f;
        }
        else if(wantToOpen) {
            wantToOpen = false;
            RaycastHit hit;
            if(Physics.SphereCast(playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, playerCam.nearClipPlane)), 1, playerCam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)).direction, out hit, 5)) {
                ChestCollider chestHit = hit.collider.gameObject.GetComponent<ChestCollider>();
                if(chestHit != null) {
                    Time.timeScale = 0.1f;
                    lastChestOpen = chestHit;
                    Weapon info = chestHit.getWeaponInfo();
                    chestGUI.SetActive(true);
                    chestGUI.GetComponent<DisplayChest>().passWeapon(gameObject.GetComponentInChildren<PlayerWeapon>().equipped, info);
                    open = true;
                }
            }
            
        }
    }
}
