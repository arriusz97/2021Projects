﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InforType{ Item, Tree, ItemBox, Campfire}

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
   
    [SerializeField]
    private GameObject inventoryScreen, craftingScreen, campfireScreen;
    public InventoryObject inventory, quickSlot, crafting, campfire;

    [SerializeField]
    private TimerController timer;

    [SerializeField]
    private int treeLoggingTime = 3;

    public bool inventoryOpen = false, campfireOpen = false, CraftingOpen = false, playerLock = false;

    [SerializeField]
    private DataController dataController;

    [SerializeField]
    private PasueMenu pasueMenu;

    [SerializeField]
    private ItemEffectDatabase effectDatabase;

    [SerializeField]
    private GameObject UI;

    //게임 시작시 인벤토리가 한번 활성화되야해서 활성화 된채로 시작해 비활성화 시킨다.
    private void Start()
    {
        CloseInventory();
        crafting.Load();
        CloseCrafting();
        CloseCampfire();

        UI = GameObject.Find("GUI").transform.Find("UI").gameObject;
        actionText = UI.transform.Find("CursorOnItemText").transform.Find("ActionText").GetComponent<TextMeshProUGUI>();
        pasueMenu = UI.transform.Find("PauseMenu").GetComponent<PasueMenu>();

        timer = FindObjectOfType<TimerController>();
        timer.StartTimer(0);
        timer.StartTimer(1);
        timer.StopTimer(2);
        timer.TimerContainer[2].Activated(false);
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
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (campfireOpen)
            {
                CloseCampfire();
            }
            else
                pasueMenu.Escape();
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
            else if (hitInfo.transform.tag == "Interaction")
            {
                if(hitInfo.transform.GetComponent<InteractionObject>().InteractionType == eInteractionType.Tree)
                {
                    ItemInfoAppear(InforType.Tree);
                }
                else if (hitInfo.transform.GetComponent<InteractionObject>().InteractionType == eInteractionType.ItemBox)
                {
                    ItemInfoAppear(InforType.ItemBox);
                }
                else if (hitInfo.transform.GetComponent<InteractionObject>().InteractionType == eInteractionType.Campfire)
                {
                    if (campfireOpen)
                    {
                        ItemInfoDisappear();
                    }
                    ItemInfoAppear(InforType.Campfire);
                }
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
        else if(type == InforType.ItemBox)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Root ";
        }
        else if(type == InforType.Campfire)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Cook ";
        }
        
    }
    //아이템 문구 비활성화
    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }
    //아이템인지 판단하고 맞다면 작동
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform.tag == "Item")        //태그가 아이템일 경우 
            {
                Item _item = new Item(hitInfo.transform.GetComponent<GroundItem>().item);

                if (hitInfo.transform != null)      //획득 후 파괴한다.
                {
                    effectDatabase.pickupSound();
                    inventory.AddItem(_item, 1);
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();
                }
            }
            else if (hitInfo.transform.tag == "Interaction")       //태그가 나무일 경우 일정시간 후 파괴한다.
            {
                InteractionObject interactionObject = hitInfo.transform.GetComponent<InteractionObject>();
                if (interactionObject.InteractionType == eInteractionType.Tree)
                {
                    CoconutTree coconutTree = hitInfo.transform.GetComponent<CoconutTree>();
                    coconutTree.ActionClockOn(treeLoggingTime);
                    coconutTree.TreeFall(treeLoggingTime);
                    ItemInfoDisappear();
                }
                else if (interactionObject.InteractionType == eInteractionType.ItemBox)
                {
                    interactionObject.ActionClockOn(3);
                    interactionObject.ItemRoot(inventory);
                    ItemInfoDisappear();
                }
                else if (interactionObject.InteractionType == eInteractionType.Campfire)
                {
                    OpenCamfire();
                }
            }
        }
    }
    //인벤토리 창 활성화
    public void OpenInventory()
    {
        inventoryOpen = true;
        inventoryScreen.SetActive(true);
        PlayLock();
    }
    //인벤토리 창 비활성화
    public void CloseInventory()
    {
        inventoryOpen = false;
        inventoryScreen.GetComponent<UserInterface>().theItemEffectDatabase.HideToolTip();
        inventoryScreen.SetActive(false);
        PlayLock();

    }
    //제작 창 활성화
    public void OpenCrafting()
    {
        CraftingOpen = true;
        craftingScreen.SetActive(true);
        PlayLock();
    }
    //제작 창 비활성화
    public void CloseCrafting()
    {
        CraftingOpen = false;
        craftingScreen.SetActive(false);
        PlayLock();
    }

    public void OpenCamfire()
    {
        campfireOpen = true;
        campfireScreen.SetActive(true);
        PlayLock();
    }

    public void CloseCampfire()
    {
        campfireOpen = false;
        campfireScreen.SetActive(false);
        PlayLock();
    }

    private void PlayLock()
    {
        if (CraftingOpen == false && inventoryOpen == false && campfireOpen == false)
        {
            playerLock = false;
        }
        else
            playerLock = true;
    }

    //종료시 인벤토리와 퀵슬롯을 초기화한다.
    public void OnApplicationQuit()
    {
        OnQuit();
    }

    public void OnQuit()
    {
        inventory.Clear();
        quickSlot.Clear();
    }

    public void Lock(bool Lock)
    {
        playerLock = Lock;
    }

    public void SaveGame()
    {
        inventory.Save();
        quickSlot.Save();
        crafting.Save();
        campfire.Save();
    }

    public void LoadGame()
    {
        inventory.Load();
        quickSlot.Load();
        crafting.Load();
        campfire.Load();
    }
}