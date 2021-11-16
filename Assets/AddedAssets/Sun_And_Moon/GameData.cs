using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[Serializable]
public class GameData
{
    public int currentDay;

    //Player
    public float playerHP, playerTP;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public int[] controlGuideBoolean = new int[8];
    public int O2upgrade = 0;
    public int swimUpgrade = 0;

    public bool[] consExit = new bool[3];
    public Vector3[] consPosition = new Vector3[3];
    public Quaternion[] consRotation = new Quaternion[3];
}