using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Spells", menuName = "Spells/FlyingShadow")]
public class FlyingFastSpell : Spell
{

    public override void fire(SpellMod modifiers, Vector3 firePoint, Vector3 target) {
        GameObject temp = Instantiate(projectile) as GameObject;
        temp.transform.position = firePoint;

        ISpellCollision spellScript = temp.GetComponent<ISpellCollision>();
        spellScript.setModifiers(modifiers);

        Rigidbody projectRigid = temp.GetComponent<Rigidbody>();
        projectRigid.velocity = (target - firePoint).normalized * 20;
    }

    public override void fire(SpellMod modifiers, Transform firePoint, Camera mainCam) {
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
