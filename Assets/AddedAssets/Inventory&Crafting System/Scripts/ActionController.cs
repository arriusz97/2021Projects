using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InforType{ Item, Tree, }

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickupActivated = false;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private TextMeshProUGUI actionText;

    public InventoryObject inventory, quickSlot, crafting;

    [SerializeField]
    private GameObject inventoryScreen, craftingScreen;

    [SerializeField]
    private TimerController timer;

    [SerializeField]
    private int treeLoggingTime = 3;

    private bool inventoryOpen = false;
    private bool CraftingOpen = false;



    //게임 시작시 인벤토리가 한번 활성화되야해서 활성화 된채로 시작해 비활성화 시킨다.
    private void Start()
    {
        CloseInventory();
        crafting.Load();
        CloseCrafting();
    }
    
    void Update()
    {
        CheckItem();
        TryAction();

        //단축키 i를 입력받으면 인벤토리 창을 띄우거나 닫는다.
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
        //단축키 c를 입력받으면 제작 창을 띄우거나 닫는다.
        else if(Input.GetKeyDown(KeyCode.C))
        {
            if (CraftingOpen)
            {
                CloseCrafting();
            }
            else
            {
                OpenCrafting();
            }
        }
        else if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            inventory.Save();
            quickSlot.Save();
            crafting.Save();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventory.Load();
            quickSlot.Load();
            crafting.Load();
        }
    }
    //단축키 F를 입력받으면 아이템을 주울 수 있는지 판단하고 가능하면 줍는다.
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckItem();
            CanPickUp();
        }
    }
    //카메라가 바라보는 방향으로 설정한 거리안에 오브젝트가 있다면 아이템인지 판단하고 안내문구를 띄운다.
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear(InforType.Item);
            }
            else if (hitInfo.transform.tag == "Tree")
            {
                ItemInfoAppear(InforType.Tree);
            }
        }
        else
            ItemInfoDisappear();
    }
    //아이템 문구 활성화
    private void ItemInfoAppear(InforType type)
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        if(type == InforType.Item)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to pick up " + hitInfo.transform.GetComponent<GroundItem>().item.name;
        }
        else if(type == InforType.Tree)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to logging " ;
        }
        
    }
    //아이템 문구 비활성화
    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    //아이템인지 판단하고 맞다면 획득후 파괴한다.
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform.tag == "Item")
            {
                Item _item = new Item(hitInfo.transform.GetComponent<GroundItem>().item);

                if (hitInfo.transform != null)
                {
                    inventory.AddItem(_item, 1);
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();
                }
            }
            else if (hitInfo.transform.tag == "Tree")
            {
                timer.ActionClockOn(treeLoggingTime);
                Destroy(hitInfo.transform.gameObject, treeLoggingTime);
            }
        }
    }
    //인벤토리 창 활성화
    private void OpenInventory()
    {
        inventoryOpen = true;
        inventoryScreen.SetActive(true);
    }
    //인벤토리 창 비활성화
    private void CloseInventory()
    {
        inventoryOpen = false;
        inventoryScreen.SetActive(false);
    }
    private void OpenCrafting()
    {
        CraftingOpen = true;
        craftingScreen.SetActive(true);
    }
    //인벤토리 창 비활성화
    private void CloseCrafting()
    {
        CraftingOpen = false;
        craftingScreen.SetActive(false);
    }

    //종료시 인벤토리와 퀵슬롯을 초기화한다.
    public void OnApplicationQuit()
    {
        inventory.Clear();
        quickSlot.Clear();
    }
}