using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterJpystick : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //print("Jright=" + Input.GetAxis("Jright"));
        print("LT/RT=" + Input.GetAxis("LT/RT"));
        //print("RB:" + Input.GetButtonDown("RB"));
    }
}
