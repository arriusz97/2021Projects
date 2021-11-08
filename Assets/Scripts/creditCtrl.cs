using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditCtrl : MonoBehaviour
{
    public RawImage m_creditImage;
    public RectTransform m_rectTransform;
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private AudioSource m_bgm;
    [SerializeField]
    private AudioSource m_helicopter;

    private void Awake()
    {
        m_bgm.Play();
        m_helicopter.Play();
    }

    // Update is called once per frame
    void Update()
    {
        m_rectTransform.position = new Vector3(0, m_rectTransform.position.y + m_Speed, m_rectTransform.position.z);

        Debug.Log(m_rectTransform.position.y + m_Speed);


        if(m_rectTransform.position.y + m_Speed > -1800f)
        {
            StartCoroutine(twosec());
        }
    }

    IEnumerator twosec()
    {
        WaitForSeconds two = new WaitForSeconds(2f);
        yield return two;

        m_bgm.Stop();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
