using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    //물고기와의 상호작용 (잡아 먹을 수 있는) Ctrl

    private void OnTriggerStay(Collider other)
    {
        //만약 amberjack과 충돌하고 f키를 눌렀다면 습득
        if (other.gameObject.CompareTag("Amberjack"))
        {
            if (Input.GetKey(KeyCode.F))
            {
                other.SendMessage("DIE");
                other.gameObject.SetActive(false);
                Debug.Log("aquire Amberjack");
            }
        }
    }
}
