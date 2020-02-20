using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AI : ScriptableObject {
    public abstract void nextUpdate(GameObject avatar, EnemyStats stats);
}
