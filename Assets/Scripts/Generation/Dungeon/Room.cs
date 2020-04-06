using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Transform[] exits;
    private Transform[] volume;

    private List<SpawnManager> spawnPoints;
    //Initialise on instantiation
    void Awake()
    {
        Transform nextObj;
        Transform thisObject = GetComponent<Transform>();
        for(int i = 0; i < thisObject.childCount; i ++) {
            nextObj = thisObject.GetChild(i);
            if(nextObj.gameObject.name.Equals("Exits")) {
                populateArray(nextObj, 0);
            }
            else if(nextObj.gameObject.name.Equals("Voxels")) {
                populateArray(nextObj, 1);
            }
            else if(nextObj.gameObject.name.Equals("SpawnPoints")) {
                spawnPoints = new List<SpawnManager>(nextObj.childCount);
                spawnPoints.AddRange(nextObj.GetComponentsInChildren<SpawnManager>());
            }
        }
        if(exits == null || volume == null) {
            Debug.LogError("Exits or voxel volume have not been defined!!");
        }
    }

    private void populateArray(Transform parent, int type) {
        Transform[] toPopulate = null;
        if(type == 1) {
            volume = new Transform[parent.childCount];
            toPopulate = volume;
        }
        else if(type == 0) {
            exits = new Transform[parent.childCount];
            toPopulate = exits;
        }

        for(int i = 0; i < parent.childCount; i++) {
            toPopulate[i] = parent.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform[] getExits() {
        return exits;
    }

    public Transform[] getVoxels() {
        return volume;
    }

    public List<SpawnManager> getSpawnPoints() { 
        return spawnPoints;
    }

    public Transform[] getOpenExits() {
        List<Transform> open = new List<Transform>();
        foreach(Transform t in exits) {
            if(!t.gameObject.GetComponent<Exit>().isClosed()) {
                open.Add(t);
            }
        }
        return open.ToArray();
    }

    public int getNumOpenExits() {
        int open = 0;
        foreach(Transform t in exits) {
            if(!t.gameObject.GetComponent<Exit>().isClosed()) {
                open += 1;
            }
        }
        return open;
    }
}
