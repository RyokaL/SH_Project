using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GenSettings : MonoBehaviour
{
    public InputField seed;
    public Slider rooms;

    public int seedVal;
    public int maxRooms;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void startGame() {
        seedVal = 0;
        maxRooms = (int)rooms.value;

        if(seed.text != "") {
            if(!Int32.TryParse(seed.text, out seedVal)) {
                seedVal = 0;
                foreach(char c in seed.text) {
                    seedVal += c;
                }   
            }
        }
        else {
            seedVal = (int)System.DateTime.Now.Ticks;
        }
        SceneManager.LoadScene("GenerationTest", LoadSceneMode.Single);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
