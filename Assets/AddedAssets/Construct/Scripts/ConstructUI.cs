using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Construct
{
    public string craftName; // 이름
    public GameObject go_prefab; // 실제 설치 될 프리팹
    public GameObject go_PreviewPrefab; // 미리 보기 프리팹
    public ItemObject IO;
    public GameObject obj;
}

public class ConstructUI : MonoBehaviour
{
    private bool isActivated = false;  // CraftManual UI 활성 상태
    private bool isPreviewActivated = false; // 미리 보기 활성화 상태

    [SerializeField]
    private GameObject go_BaseUI; // 기본 베이스 UI

    [SerializeField]
    private Construct[] constructs;  // 불 탭에 있는 슬롯들. 

    private GameObject go_Preview; // 미리 보기 프리팹을 담을 변수
    private GameObject go_Prefab; // 실제 생성될 프리팹을 담을 변수 

    [SerializeField]
    private Transform tf_Player;  // 플레이어 위치

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float range;

    [SerializeField]
    private int ConstructionNUm;

    [SerializeField]
    private DataController dataController;

    [SerializeField]
    private ActionController AC;

    public ItemEffectDatabase theItemEffectDatabase;

    public bool[] mConsExit = new bool[3];
    public Vector3[] mConsPosition = new Vector3[3];
    public Quaternion[] mConsRotation = new Quaternion[3];

    private void OnEnable()
    {
        theItemEffectDatabase = GameObject.Find("EffectDatabase").GetComponent<ItemEffectDatabase>();
    }

    public void SlotClick(int _slotNumber)
    {
        theItemEffectDatabase.HideToolTip();
        go_Preview = Instantiate(constructs[_slotNumber].go_PreviewPrefab, tf_Player.position + tf_Player.forward, Quaternion.identity);
        go_Prefab = constructs[_slotNumber].go_prefab;
        isPreviewActivated = true;
        go_BaseUI.SetActive(false);
        ConstructionNUm = _slotNumber;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isPreviewActivated)
            Window();

        if (isPreviewActivated)
            PreviewPositionUpdate();

        if (Input.GetButtonDown("Fire1"))
            Build();

        if (Input.GetButtonDown("Fire2"))
            Cancel();
    }

    private void PreviewPositionUpdate()
    {
        if (Physics.Raycast(tf_Player.position, tf_Player.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null)
            {
                Vector3 _location = hitInfo.point;
                go_Preview.transform.position = _location;
            }
        }
    }

    private void Build()
    {
        if (isPreviewActivated && go_Preview.GetComponent<Preview>().isBuildable() && constructs[ConstructionNUm].IO.CanCraft())
        {
            GameObject cons = Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
            //Instantiate(go_Prefab, hitInfo.point, Quaternion.identity);
            Destroy(go_Preview);
            isActivated = false;
            isPreviewActivated = false;
            go_Preview = null;
            go_Prefab = null;
            constructs[ConstructionNUm].IO.RemoveIngredientsFromInventory();

            mConsExit[ConstructionNUm] = true;
            mConsPosition[ConstructionNUm] = cons.transform.position;
            mConsRotation[ConstructionNUm] = cons.transform.rotation;
        }
    }

    public void Window()
    {
        if (!isActivated)
        {
            OpenWindow();
            AC.OpenConstruct();
        }
        else
        {
            CloseWindow();
            AC.CloseConstruct();
        }
    }

    private void OpenWindow()
    {
        isActivated = true;
        go_BaseUI.SetActive(true);
    }

    private void CloseWindow()
    {
        theItemEffectDatabase.HideToolTip();
        isActivated = false;
        go_BaseUI.SetActive(false);
    }

    private void Cancel()
    {
        if (isPreviewActivated)
            Destroy(go_Preview);

        isActivated = false;
        isPreviewActivated = false;

        go_Preview = null;
        go_Prefab = null;

        go_BaseUI.SetActive(false);
    }

    public void OnPointerEnter(int _slotNumber)
    {
        ConstructionNUm = _slotNumber;
        theItemEffectDatabase.ShowToolTip(constructs[ConstructionNUm].IO, constructs[ConstructionNUm].obj.transform.position);
    }

    public void OnPinterExit()
    {
        theItemEffectDatabase.HideToolTip();
    }

    public void ConstructionLoad()
    {
        for(int i = 0; i < 3; i++)
        {
            if(mConsExit[i])
                Instantiate(constructs[i].go_prefab, mConsPosition[i], mConsRotation[i]);
        }
    }
}