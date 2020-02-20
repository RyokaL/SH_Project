using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Spells", menuName = "Spells/EnemyShadow")]
public class EnemyNormal : Spell
{

    public override void fire(SpellMod modifiers, Transform firePoint, Transform target) {
        GameObject temp = Instantiate(projectile) as GameObject;
        temp.transform.position = firePoint.position;

        ISpellCollision spellScript = temp.GetComponent<ISpellCollision>();
        spellScript.setModifiers(modifiers);

        Rigidbody projectRigid = temp.GetComponent<Rigidbody>();
        projectRigid.velocity = (target.position - firePoint.position).normalized * 5;
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
