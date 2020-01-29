using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Extensions;

public class DungeonCreator : MonoBehaviour
{
    [Tooltip("List of all rooms to consider")]
    public List<GameObject> rooms;
    [Tooltip("List of rooms with spawn points")]
    public List<GameObject> spawnRooms;
    public int gridSpacing;
    public int maxRooms;
    //Store grid position along with voxel object
    private Dictionary<Vector3, Transform> grid;
    private List<Room> roomsToProcess;
    private List<GameObject> actualRooms;

    private int roomsSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Setup and spawn initial room
        grid = new Dictionary<Vector3, Transform>();
        roomsToProcess = new List<Room>();
        actualRooms = new List<GameObject>();
    
        //Spawn initial room
        GameObject roomToSpawn = spawnRooms[(int)Random.Range(0, spawnRooms.Count)];
        GameObject newRoomObj = Instantiate(roomToSpawn, transform.position, transform.rotation) as GameObject;
        Room currRoom = newRoomObj.GetComponent<Room>();

        fillGrid(currRoom.getVoxels());

        if(currRoom.getNumOpenExits() > 0) {
            roomsToProcess.Add(currRoom);
        }

        actualRooms.Add(newRoomObj);

        var stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        generateMap();
        stopwatch.Stop();
        Debug.Log("Time taken: " + stopwatch.ElapsedMilliseconds);
    }

    private void generateMap() {
        //Worry about closing off unfinished exits later :)
         //TODO: Add in different voxel size doors?
        GameObject empty;
        while(roomsSpawned < maxRooms && roomsToProcess.Count > 0) {
            roomsToProcess.Randomise();
            //Choose a room to process, if all exits are closed, remove and continue
            Room linkRoom = roomsToProcess[(int)Random.Range(0, roomsToProcess.Count)];
            roomsToProcess.Remove(linkRoom);

            if(linkRoom.getNumOpenExits() <= 0) {
               continue;
            }
            //Pick an exit from the already existing room
            Transform[] linkExits = linkRoom.getOpenExits();
            Transform exitExisting = linkExits[(int)Random.Range(0, linkExits.Length)];

            //Now randomly choose a room to attach
            GameObject roomToSpawn = rooms[(int)Random.Range(0, rooms.Count)];
            GameObject newRoomObj = Instantiate(roomToSpawn, transform.position, transform.rotation) as GameObject;
            Room currRoom = newRoomObj.GetComponent<Room>();

            //If both have only 1 exit available and we have other rooms, try again
            if(currRoom.getNumOpenExits() == 1 && linkRoom.getNumOpenExits() == 1 && (roomsToProcess.Count > 0 || roomsSpawned < maxRooms / 2)) {
                Destroy(newRoomObj);
                roomsToProcess.Add(linkRoom);
                continue;
            }

            //Pick an exit from this room to link up
            Transform[] currExits = currRoom.getOpenExits();
            Transform exitNew = currExits[(int)Random.Range(0, currExits.Length)];

            empty = new GameObject();
            empty.transform.position = exitNew.position;
            //empty.transform.rotation = newRoomObj.transform.rotation;
            newRoomObj.transform.parent = empty.transform;
            // empty.transform.rotation = exitExisting.transform.rotation;
            if(exitNew.right != exitExisting.right) {
                empty.transform.rotation = empty.transform.rotation * (Quaternion.FromToRotation(exitNew.right, exitExisting.right * -1));  
            } 
            //empty.transform.right = Vector3.Reflect(exitNew.transform.right, Vector3.right);
            //empty.transform.rotation = Quaternion.Inverse(exitExisting.transform.rotation);
            empty.transform.position = exitExisting.position;

            Transform[] roomVoxels = currRoom.getVoxels();
            Vector3[] newVoxels = new Vector3[roomVoxels.Length];

            for(int i = 0; i < roomVoxels.Length; i++) {
                Transform v = roomVoxels[i];
                Vector3 vPos = v.position;
                //new Vector3(v.position.x, v.position.y, v.position.z);
                Vector3 exitDir = exitNew.right;

                if((vPos.x % (gridSpacing / 2)) == 0 && !((vPos.x % gridSpacing) == 0)) {
                    //{1, 0, 0} <-
                    if(exitDir == (Vector3.right)) {
                        vPos.x = vPos.x - (gridSpacing / 2);
                    }
                    //{-1, 0, 0} ->
                    else if(exitDir == (Vector3.left)) {
                        vPos.x = vPos.x + (gridSpacing / 2);
                    }
                }

                if((vPos.z % (gridSpacing / 2)) == 0 && ! ((vPos.z % gridSpacing) == 0)) {
                    //{0, 0, 1} ^
                    if(exitDir == (Vector3.forward)) {
                        vPos.z = vPos.z - (gridSpacing / 2);
                    }
                    //{0, 0, -1} v
                    else if(exitDir == (Vector3.back)) {
                        vPos.z = vPos.z + (gridSpacing / 2);
                    }
                }
                
                if((vPos.y % (gridSpacing / 2)) == 0 && ! ((vPos.y % gridSpacing) == 0)) {
                    //{0, 1, 0} /'
                    if(exitDir == (Vector3.up)) {
                        vPos.y = vPos.y - (gridSpacing / 2);
                    }
                    //{0, -1, 0} ./
                    else if(exitDir == (Vector3.down)) {
                        vPos.y = vPos.y + (gridSpacing / 2);
                    }
                }
                newVoxels[i] = vPos;
            }

            if(isColliding(newVoxels)) {
                Destroy(newRoomObj);
                Destroy(empty);
                empty = null;
            }
            else {
                fillGrid(roomVoxels, newVoxels);
                exitNew.GetComponent<Exit>().setClosed();
                exitExisting.GetComponent<Exit>().setClosed();

                if(currRoom.getNumOpenExits() > 0) {
                    roomsToProcess.Add(currRoom);
                }

                actualRooms.Add(empty);
                roomsSpawned++;
            }

            if(linkRoom.getNumOpenExits() > 0) {
                roomsToProcess.Add(linkRoom);
            }
        }
        tidyUpEditor();
        closeDoors();
    }

    private void closeDoors() {
        //TODO: Check if we're connected to a different open exit, randomly leave some open
        List<Transform> allExits = actualRooms.SelectMany(room => room.GetComponentInChildren<Room>().getOpenExits()).ToList(); 

        foreach(GameObject room in actualRooms) {
            Room currRoom = room.GetComponentInChildren<Room>();
            foreach(Transform exit in currRoom.getOpenExits()) {
                Exit currExit = exit.GetComponent<Exit>();

                if(currExit.isClosed()) {
                    continue;
                }
                if(currExit.getExitCover() == null) {
                    continue;
                }

                bool leaveOpen = false;

                for(int i = 0; i < allExits.Count; i++) {
                    Transform checkExit = allExits.ElementAt(i);

                    if(currExit == checkExit.gameObject.GetComponent<Exit>()) {
                        continue;
                    }

                    if(checkExit.transform.position == exit.position) {
                        //Do random check, for now just leave open anyway
                        leaveOpen = true;
                        allExits.RemoveAt(i);
                        allExits.Remove(exit);
                        break;
                    }
                }

                if(leaveOpen) {
                    continue;
                }

                GameObject cover = Instantiate(currExit.getExitCover(), transform.position, transform.rotation) as GameObject;
                GameObject empty = new GameObject();
                
                empty.transform.position = cover.transform.Find("Connector").position;
                cover.transform.parent = empty.transform;

                empty.transform.rotation = empty.transform.rotation * (Quaternion.FromToRotation(cover.transform.right, exit.right));
                empty.transform.position = exit.position;

                empty.transform.parent = exit;
                currExit.setClosed();
            }
        }
    }

    private void fillGrid(Transform[] voxels) {
        foreach(Transform t in voxels) {
            if(grid.ContainsKey(convertRealPosToGrid(t.position))) {
                //Debug.Log("Bad");
            }
            grid.Add(convertRealPosToGrid(t.position), t);
        }
    }

    private void fillGrid(Transform[] voxels, Vector3[] gridVox) {
        for(int i = 0; i < gridVox.Length; i++) {
            if(grid.ContainsKey(convertRealPosToGrid(gridVox[i]))) {
                //Debug.Log("Bad");
            }
            grid.Add(convertRealPosToGrid(gridVox[i]), voxels[i]);
        }
    }

    private bool isColliding(Vector3[] newPos) {
        foreach(Vector3 v in newPos) {
            Vector3 gridPos = convertRealPosToGrid(v);
            if(grid.ContainsKey(gridPos)) {
                return true;
            }
        }
        return false;
    }

    private Vector3 convertRealPosToGrid(Vector3 real) {
        int gridX = Mathf.RoundToInt(real.x / gridSpacing);
        int gridY = Mathf.RoundToInt(real.y / gridSpacing);
        int gridZ = Mathf.RoundToInt(real.z / gridSpacing);
        return new Vector3(gridX, gridY, gridZ);
    }

    private void tidyUpEditor() {
        //Set all objects' parent to this
        foreach(GameObject g in actualRooms) {
            g.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {}
}