using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private GameObject go_Base;

    [SerializeField]
    private Text txt_ItemName;
    [SerializeField]
    private Text txt_ItemDesc;
    [SerializeField]
    private Text txt_ItemHowtoUsed;

    //툴팁을 마우스커서 기준으로 아래에 출력시킨다
    public void ShowToolTip(ItemObject _item, Vector3 _pos)
    {
        go_Base.SetActive(true);

        if (_item.name == "Recipe Campfire" || _item.name == "Recipe Shelter" || _item.name == "Recipe Tent")
        {
            _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0f,
                                -go_Base.GetComponent<RectTransform>().rect.height * 0.95f, 0);
        }
        else
        {
            _pos += new Vector3(go_Base.GetComponent<RectTransform>().rect.width * 0f,
                                -go_Base.GetComponent<RectTransform>().rect.height * 0.55f, 0);
        }
        go_Base.transform.position = _pos;

        txt_ItemName.text = _item.data.Name;
        txt_ItemDesc.text = _item.description;


        //txt_ItemHowtoUsed.text = "";      추후 각 아이템별로 사용법을 추가하기위해 만들어둠
    }

    public void HideToolTip()
    {
        go_Base.SetActive(false);
    }
}
