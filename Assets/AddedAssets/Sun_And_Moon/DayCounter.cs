using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI day;
    public void DayUpdate(int currentDay) //DayCounter의 Text에 현재날짜를 업데이트한다.
    {
        day.transform.localPosition = new Vector3(day.transform.localPosition.x, 92.317f, 0f);
        day.text = (currentDay + 1).ToString() + "\u000a" + currentDay.ToString();
    }

    public void StartCounter()
    {
        day.transform.Translate(Vector3.down * 100f * Time.deltaTime);
        float x = day.transform.localPosition.x;
        float y = Mathf.Clamp(day.transform.localPosition.y, -92.317f, 92.317f);
        day.transform.localPosition = new Vector3(x, y, 0f);
        //DayCounter의 날짜를 마스크 범위내에서 천천히 하강시킨다.
    }
}