using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private float rotaionSpeed;

    private float time;

    private void Update()
    {
        time += Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, -rotaionSpeed * time);
    }
}
