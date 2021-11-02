﻿using System.Collections;
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
    public constructionObject[] CObjects = new constructionObject[5];
    public int[] controlGuideBoolean = new int[8];
    public int O2upgrade = 0;
    public int swimUpgrade = 0;
}

public class constructionObject
{
    public Vector3 objectPosition;
    public Quaternion objectRotation;
}