using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutTree : InteractionObject
{
    [SerializeField]
    private GameObject CoconutPrefabs, LogPrefabs;

    private AudioSource treeFalldown;

    private void Awake()
    {
        treeFalldown = GetComponent<AudioSource>();
    }

    public void TreeFall()        
    {
        interactionReady = false;
        StartCoroutine(LogCoroutine(interactionTime));
    }

    IEnumerator LogCoroutine(int LoggingTime)
    {
        yield return new WaitForSeconds(LoggingTime);
        //상호작용 시간동안 대기
        treeFalldown.Play();

        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //나무의 고정을 해제하고
        Instantiate(CoconutPrefabs, gameObject.transform.position + (gameObject.transform.up * 8f), Quaternion.LookRotation(gameObject.transform.up));
        Instantiate(CoconutPrefabs, gameObject.transform.position + (gameObject.transform.up * 9f), Quaternion.LookRotation(gameObject.transform.up));
        Instantiate(CoconutPrefabs, gameObject.transform.position + (gameObject.transform.up * 10f), Quaternion.LookRotation(gameObject.transform.up));
        Instantiate(LogPrefabs, gameObject.transform.position + (gameObject.transform.up * 5f), Quaternion.LookRotation(gameObject.transform.up));
        //아이템을 생성한 후
        Destroy(gameObject, 4);
        //4초 후 파괴한다.
    }
}