using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AI : MonoBehaviour {
    public abstract void nextUpdate(GameObject avatar, EnemyStats stats);
}
