using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum InforType{ Item, Tree, ItemBox, Campfire, Construction, Tent}

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;
    public bool pickupActivated = false;
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

    public bool inventoryOpen = false, campfireOpen = false, CraftingOpen = false, ConstructOpen = false, playerLock = false;

    [SerializeField]
    private DataController dataController;

    [SerializeField]
    private PasueMenu pasueMenu;

    [SerializeField]
    private GameObject pauseMenu_gameObject;

    public ItemEffectDatabase effectDatabase;

    [SerializeField]
    private GameObject UI;

    [SerializeField]
    private player mPlayer;

    [SerializeField]
    private SunController SC;

    //게임 시작시 인벤토리가 한번 활성화되야해서 활성화 된채로 시작해 비활성화 시킨다.
    private void Start()
    {
        CloseInventory();
        crafting.Load();
        CloseCrafting();
        campfire.Load();
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
                Cursor.visible = false;
            }
            else
            {
                OpenInventory();
                Cursor.visible = true;
            }
        }
        //단축키 c를 입력받으면 제작 창을 띄우거나 닫는다.
        else if(Input.GetKeyDown(KeyCode.C))
        {
            if (CraftingOpen)
            {
                CloseCrafting();
                Cursor.visible = false;
            }
            else
            {
                OpenCrafting();
                Cursor.visible = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (campfireOpen)
            {
                CloseCampfire();
                Cursor.visible = false;
            }
            else
            {
                if (pauseMenu_gameObject.gameObject.activeInHierarchy)
                {
                    Cursor.visible = false;
                }
                pasueMenu.Escape();
            }
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
                else if (hitInfo.transform.GetComponent<InteractionObject>().InteractionType == eInteractionType.Construction)
                {
                    ItemInfoAppear(InforType.Construction);
                }
                else if (hitInfo.transform.GetComponent<InteractionObject>().InteractionType == eInteractionType.Tent)
                {
                    ItemInfoAppear(InforType.Tent);
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
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Pick Up " + hitInfo.transform.GetComponent<GroundItem>().item.name;
        }
        else if(type == InforType.Tree)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Logging " ;
        }
        else if(type == InforType.ItemBox)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Root ";
        }
        else if(type == InforType.Campfire)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Cook ";
        }
        else if(type == InforType.Construction)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Dismantle ";
        }
        else if (type == InforType.Tent)
        {
            actionText.text = "Press " + "<color=yellow>" + "(F)" + "</color>" + " to Sleep ";
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
                    effectDatabase.PickupSound();
                    inventory.AddItem(_item, 1);
                    Destroy(hitInfo.transform.gameObject);
                    ItemInfoDisappear();
                }
            }
            else if (hitInfo.transform.tag == "Interaction")
            {
                InteractionObject interactionObject = hitInfo.transform.GetComponent<InteractionObject>();
                if (interactionObject.getReady())
                {
                    if (interactionObject.InteractionType == eInteractionType.Tree)
                    {
                        CoconutTree coconutTree = hitInfo.transform.GetComponent<CoconutTree>();
                        coconutTree.ActionClockOn();
                        coconutTree.TreeFall();
                        ItemInfoDisappear();
                    }
                    else if (interactionObject.InteractionType == eInteractionType.ItemBox)
                    {
                        interactionObject.ActionClockOn();
                        interactionObject.ItemRoot(inventory);
                        ItemInfoDisappear();
                    }
                    else if (interactionObject.InteractionType == eInteractionType.Campfire)
                    {
                        OpenCamfire();
                    }
                    else if (interactionObject.InteractionType == eInteractionType.Construction)
                    {
                        interactionObject.Dismantle();
                    }
                    else if (interactionObject.InteractionType == eInteractionType.Tent)
                    {
                        SC.Sleep();
                    }
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
        Cursor.visible = true;
    }
    //제작 창 비활성화
    public void CloseCrafting()
    {
        CraftingOpen = false;
        craftingScreen.GetComponent<UserInterface>().theItemEffectDatabase.HideToolTip();
        craftingScreen.SetActive(false);
        PlayLock();
        Cursor.visible = false;
    }

    public void OpenCamfire()
    {
        campfireOpen = true;
        campfireScreen.SetActive(true);
        PlayLock();
        Cursor.visible = true;
    }

    public void CloseCampfire()
    {
        campfireOpen = false;
        campfireScreen.SetActive(false);
        PlayLock();
        Cursor.visible = false;
    }

    public void OpenConstruct()
    {
        ConstructOpen = true;
        PlayLock();
        Cursor.visible = true;
    }

    public void CloseConstruct()
    {
        ConstructOpen = false;
        PlayLock();
        Cursor.visible = false;
    }

    private void PlayLock()
    {
        if (ConstructOpen)
        {
            playerLock = false;
        }
        else if (CraftingOpen == false && inventoryOpen == false && campfireOpen == false)
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

        if(dataController.Gamedata.O2upgrade == 1)
        {
            O2gaugeUpgrade();
        }
        if(dataController.Gamedata.swimUpgrade == 1)
        {
            SwimSpeedUpgrade();
        }
        if (mPlayer.transform.position.y <= -1.9)
        {
            mPlayer.m_SwimTrigger.m_isWater = true;
        }
    }

    public void O2gaugeUpgrade()
    {
        timer.TimerContainer[2].duration = timer.TimerContainer[2].duration + 30;
        dataController.Gamedata.O2upgrade = 1;
    }

    public void SwimSpeedUpgrade()
    {
        mPlayer.SwimSpeedUpgrade();
        dataController.Gamedata.swimUpgrade = 1;
    }
}

////pause menu를 닫으면 mouse cursor 끄기