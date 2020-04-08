using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayChest : MonoBehaviour
{
    public Text weaponInfoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void passWeapon(Weapon current, Weapon info) {
        string weaponInfo = "";
        SpellMod spellInfo = info.modifiers;
        weaponInfo += info.spellPrefab.spellName + "\n";
        
        float damageDiff = spellInfo.damage - current.modifiers.damage;
        string damageDiffStr = " (" + (damageDiff < 0 ? "-" : "+") + damageDiff + ")"; 

        weaponInfo += "Damage: " + spellInfo.damage + damageDiffStr + "\n";

        if(spellInfo.dot) {
            weaponInfo += "Applies DoT for " + spellInfo.dotTick + "/s for " + spellInfo.dotLength + "\n";
        }

        switch(info.spellPrefab.spellAttributes.spellType) {
            case 0:
                weaponInfo += "Fire Rate: " + spellInfo.fireRate + " shots/second" + "\n"; 
                weaponInfo += "Travel Time: " + spellInfo.TTL + "\n";
                break;
            case 1:
                weaponInfo += "Hit Rate: " + spellInfo.fireRate + " ticks/second" + "\n";
                weaponInfo += "Charge: " + spellInfo.TTL + "\n";
                weaponInfo += "Max Range Time: " + 1/spellInfo.range + "s\n";
                break;
            case 2:
                weaponInfo += "Max Charge Time: " + spellInfo.fireRate + "s\n";
                weaponInfo += "Travel Time: " + spellInfo.TTL + "\n";
                break;
            case 3:
                weaponInfo += "Fire Rate: " + spellInfo.fireRate + " shots/second" + "\n"; 
                break;
        }

        if(spellInfo.pierce) {
            weaponInfo += "(+Piercing) ";
        }
        if(spellInfo.bounce) {
            weaponInfo += "(+Bouncing Bullets) ";
        }
        weaponInfoText.text = weaponInfo;
    }
}
