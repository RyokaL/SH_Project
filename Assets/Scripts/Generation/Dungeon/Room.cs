using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private Transform[] exits;
    private Transform[] volume;
    //Initialise on instantiation
    void Awake()
    {
        Transform nextObj;
        Transform thisObject = GetComponent<Transform>();
        for(int i = 0; i < thisObject.childCount; i ++) {
            nextObj = thisObject.GetChild(i);
            if(nextObj.gameObject.name.Equals("Exits")) {
                populateArray(nextObj, false);
            }
            else if(nextObj.gameObject.name.Equals("Voxels")) {
                populateArray(nextObj, true);
            }
        }
        if(exits == null || volume == null) {
            Debug.LogError("Exits or voxel volume have not been defined!!");
        }
    }

    private void populateArray(Transform parent, bool type) {
        Transform[] toPopulate;
        if(type) {
            volume = new Transform[parent.childCount];
            toPopulate = volume;
        }
        else {
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
