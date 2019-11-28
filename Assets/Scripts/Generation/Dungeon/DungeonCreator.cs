using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<GameObject> allRooms;

    private int roomsSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Setup and spawn initial room
        grid = new Dictionary<Vector3, Transform>();
        roomsToProcess = new List<Room>();
        allRooms = new List<GameObject>();

        //Spawn initial room
        GameObject roomToSpawn = spawnRooms[(int)Random.Range(0, spawnRooms.Count)];
        GameObject newRoom = Instantiate(roomToSpawn, transform.position, transform.rotation) as GameObject;
        Room currRoom = newRoom.GetComponent<Room>();

        fillGrid(currRoom.getVoxels());

        if(currRoom.getNumOpenExits() > 0) {
            roomsToProcess.Add(currRoom);
        }

        allRooms.Add(newRoom);

        generateMap();
    }

    private void generateMap() {
        //Worry about closing off unfinished exits later :)
        while(roomsSpawned < maxRooms && roomsToProcess.Count > 0) {
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
            GameObject newRoom = Instantiate(roomToSpawn, transform.position, transform.rotation) as GameObject;
            Room currRoom = newRoom.GetComponent<Room>();
            //Pick an exit from this room to link up
            Transform[] currExits = currRoom.getOpenExits();
            Transform exitNew = currExits[(int)Random.Range(0, currExits.Length)];

            //Rotate to face link exit
            // if(exitExisting.localEulerAngles.y < 0) {
            //     newRoom.transform.Rotate(0, 180 + exitExisting.localEulerAngles.y, 0);
            // }
            // else {
            //     newRoom.transform.Rotate(0, exitExisting.localEulerAngles.y - 180, 0);
            // }

            GameObject empty = new GameObject();
            empty.transform.position = exitNew.position;
            //empty.transform.rotation = newRoom.transform.rotation;
            newRoom.transform.parent = empty.transform;
            empty.transform.rotation = exitExisting.transform.rotation;
            empty.transform.rotation = Quaternion.Inverse(exitExisting.transform.rotation);
            empty.transform.position = exitExisting.position;

            if(isColliding(currRoom.getVoxels())) {
                Destroy(empty);
            }
            else {
                exitNew.GetComponent<Exit>().setClosed();
                exitExisting.GetComponent<Exit>().setClosed();

                if(currRoom.getNumOpenExits() > 0) {
                    roomsToProcess.Add(currRoom);
                }
                roomsSpawned++;
            }

            if(linkRoom.getNumOpenExits() > 0) {
                roomsToProcess.Add(linkRoom);
            }
        }
    }

    private void fillGrid(Transform[] voxels) {
        foreach(Transform t in voxels) {
            grid.Add(convertRealPosToGrid(t.position), t);
        }
    }

    private bool isColliding(Transform[] newPos) {
        foreach(Transform t in newPos) {
            Vector3 gridPos = convertRealPosToGrid(t.position);
            if(grid.ContainsKey(gridPos)) {
                return true;
            }
        }
        return false;
    }

    private Vector3 convertRealPosToGrid(Vector3 real) {
        int gridX = (int) real.x / gridSpacing;
        int gridY = (int) real.y / gridSpacing;
        int gridZ = (int) real.z / gridSpacing;
        return new Vector3(gridX, gridY, gridZ);
    }

    private void tidyUpEditor() {
        //Set all objects' parent to this
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
