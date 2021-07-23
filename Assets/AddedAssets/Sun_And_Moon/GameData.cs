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
}