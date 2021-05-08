using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    public TextMeshProUGUI day;
    public void DayUpdate(int currentDay)
    {
        day.transform.localPosition = new Vector3(day.transform.localPosition.x, 92.317f, 0f);
        day.text = (currentDay + 1).ToString() + "\u000a" + currentDay.ToString();
    }
}
