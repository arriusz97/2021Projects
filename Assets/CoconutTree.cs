using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutTree : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider PalmCapCollider;
    

    [SerializeField]
    private GameObject CoconutPrefabs;

    public void TreeFall(int LoggingTime)
    {
        //PalmCapCollider.enabled = true;
        StartCoroutine(LogCoroutine(LoggingTime));
    }

    IEnumerator LogCoroutine(int LoggingTime)
    {
        yield return new WaitForSeconds(LoggingTime);

        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Instantiate(CoconutPrefabs, gameObject.transform.position + (gameObject.transform.up * 6f), Quaternion.LookRotation(gameObject.transform.up));
        Instantiate(CoconutPrefabs, gameObject.transform.position + (gameObject.transform.up * 8f), Quaternion.LookRotation(gameObject.transform.up));
        Instantiate(CoconutPrefabs, gameObject.transform.position + (gameObject.transform.up * 10f), Quaternion.LookRotation(gameObject.transform.up));
        
        Destroy(gameObject, 2);
    }
}
