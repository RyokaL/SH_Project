using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
[CreateAssetMenu(fileName = "Spells", menuName = "Spells/Light")]
public class LightSpell : Spell
{
    public string spellName;

    private bool fired = false;
    private Vector3 newPos;
    private GameObject currProjectile = null;

    private RaycastHit[] raycastSort(RaycastHit[] toSort, Vector3 origin) {
        float iLength, jLength;
        for(int i = 0; i < toSort.Length; i++) {
            iLength = (toSort[i].point - origin).magnitude;
            for(int j = 0; j < toSort.Length; j++) {
                jLength = (toSort[j].point - origin).magnitude;
                if(iLength < jLength) {
                    RaycastHit temp = toSort[j];
                    toSort[j] = toSort[i];
                    toSort[i] = temp;
                }
            }
        }

        return toSort;
    }

    public override void fire(SpellMod modifiers, Transform firePoint, Camera mainCam) {
        GameObject temp = Instantiate(projectile, mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCam.nearClipPlane)) - mainCam.transform.forward.normalized * 10, firePoint.rotation);
        RaycastHit[] hits;

        ISpellCollision spellCollision = temp.GetComponent<ISpellCollision>();

        temp.GetComponent<Rigidbody>().velocity = mainCam.transform.forward.normalized * 500;
        hits = Physics.SphereCastAll(mainCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, mainCam.nearClipPlane)), 0.5f, mainCam.transform.forward);
        if(hits.Length > 0) {
            hits = raycastSort(hits, firePoint.position);
            foreach(RaycastHit hit in hits) {
                if(hit.collider.tag == "Enemy") {
                    HealthControl collidedHealth = hit.collider.GetComponent<HealthControl>();
                    collidedHealth.takeDamage(modifiers.damage * modifiers.damagePercent);

                    if(modifiers.dot) {
                        collidedHealth.applyDot(modifiers.dotTick, modifiers.dotLength);
                    }
                }
                else if(hit.collider.tag == "EnemyAttack") {
                    Destroy(hit.collider.gameObject);
                }
                else if(hit.collider.gameObject.layer == 0) {
                   //modifiers.range = (hit.point - temp.transform.position).magnitude / 250;
                    return;
                }
            } 
            spellCollision.setModifiers(modifiers);
        }
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

    void LateUpdate() {

    }
}
