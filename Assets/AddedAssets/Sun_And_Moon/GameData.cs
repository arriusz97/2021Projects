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
    public constructionObject[] CObjects = new constructionObject[10];
    public int[] controlGuideBoolean = new int[10];
}

public class constructionObject
{
    public int objectID;
    public Vector3 objectPosition;
    public Quaternion objectRotation;
}