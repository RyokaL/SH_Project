using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Spells", menuName = "Spells/Lightning")]
public class Lightning : Spell
{
    public string spellName;

    public override void fire(SpellMod modifiers, Transform firePoint, Camera mainCam) {
        GameObject temp = Instantiate(projectile) as GameObject;
        temp.transform.localScale = temp.transform.localScale * modifiers.damagePercent;
        temp.transform.position = mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCam.nearClipPlane));

        ISpellCollision spellScript = temp.GetComponent<ISpellCollision>();
        spellScript.setModifiers(modifiers);

        Rigidbody projectRigid = temp.GetComponent<Rigidbody>();
        projectRigid.velocity = mainCam.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)).direction * 20;
    }

    public override void fire(SpellMod modifiers, Vector3 firePoint, Vector3 target) {
        //Implement for tracking?
        return;
    }

    public override GameObject getProjectile() {
        return projectile;
    }
    public override SpellAttr GetSpellAttr() {
        return spellAttributes;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
