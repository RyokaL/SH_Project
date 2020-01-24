using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Camera mainCam;
    private RectTransform transformThis;
    void Start() {
        mainCam = Camera.main;
        transformThis = GetComponent<RectTransform>();
    }
    // Update is called once per frame
    void Update()
    {
        //transformThis.LookAt(mainCam.transform);
    }
}
