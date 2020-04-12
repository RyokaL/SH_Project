using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused;
    public DungeonCreator seededGen;

    public InputField seedText;
    public Canvas pauseCanvas;

    public GameObject back;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void returnToMenu() {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause")) {
            if(!paused) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                paused = true;
                pauseCanvas.enabled = true;
                Time.timeScale = 0;

                seedText.text = "Seed: " + seededGen.getSeed();
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                paused = false;
                pauseCanvas.enabled = false;
                Time.timeScale = 1;
            }
        }
    }
}
