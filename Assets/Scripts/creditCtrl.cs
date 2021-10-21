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

    // Update is called once per frame
    void Update()
    {
        m_rectTransform.position = new Vector3(0, m_rectTransform.position.y + m_Speed, m_rectTransform.position.z);
    }

    IEnumerator threesec()
    {
        WaitForSeconds three = new WaitForSeconds(3f);
        yield return three;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

}
