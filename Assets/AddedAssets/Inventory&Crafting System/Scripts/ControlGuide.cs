using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlGuide : MonoBehaviour
{
    [SerializeField]
    private TypingEffect typingEffect;

    [SerializeField]
    private player player;

    [SerializeField]
    private ActionController action;

    [SerializeField]
    private int[] narrationSet = new int[8];

    public int[] guideBoolean = Enumerable.Repeat<int>(0, 8).ToArray<int>();

    public GameObject sail;    

    // Start is called before the first frame update
    void Start()
    {
        if (guideBoolean[0] == 0)
        {
            typingEffect.StartNarration(0, narrationSet[0]);
        }
        else
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if (typingEffect.waiting && typingEffect.narrationEnd)
        {
            if (guideBoolean[0] == 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    typingEffect.Skip();
                    guideBoolean[0] = 1;
                    Debug.Log("move guide clear");
                    typingEffect.StartNarration(narrationSet[0] + 1, narrationSet[1]);
                }
            }
            else if (guideBoolean[1] == 0)
            {
                if (Input.GetKey(KeyCode.LeftShift) && Input.inputString.Length == 1 && Input.inputString[0] == 'W')
                {
                    typingEffect.Skip();
                    guideBoolean[1] = 1;
                    Debug.Log("run guide clear");
                    typingEffect.StartNarration(narrationSet[1] + 1, narrationSet[2]);
                }
            }
            else if (guideBoolean[2] == 0)
            {
                if (player.transform.localPosition.y <= -2.1)
                {
                    typingEffect.Skip();
                    guideBoolean[2] = 1;
                    Debug.Log("move to sea guide clear");
                    typingEffect.StartNarration(narrationSet[2] + 1, narrationSet[3]);
                }
            }
            else if (guideBoolean[3] == 0)
            {
                if (player.transform.localPosition.y <= -3.5 && Input.GetKey(KeyCode.LeftShift))
                {
                    typingEffect.Skip();
                    guideBoolean[3] = 1;
                    Debug.Log("dive guide clear");
                    typingEffect.StartNarration(narrationSet[3] + 1, narrationSet[4]);
                }
            }
            else if (guideBoolean[4] == 0)
            {
                if (player.m_isAttack)
                {
                    typingEffect.Skip();
                    guideBoolean[4] = 1;
                    Debug.Log("attack guide clear");
                    typingEffect.StartNarration(narrationSet[4] + 1, narrationSet[5]);
                }
            }
            else if (guideBoolean[5] == 0)
            {
                if (player.transform.localPosition.y <= -2.5 && Input.GetKey(KeyCode.LeftControl))
                {
                    typingEffect.Skip();
                    guideBoolean[5] = 1;
                    Debug.Log("rise guide clear");
                    typingEffect.StartNarration(narrationSet[5] + 1, narrationSet[6]);
                }
            }
            else if (guideBoolean[6] == 0)
            {
                if (Input.GetKey(KeyCode.F) && action.pickupActivated)
                {
                    typingEffect.Skip();
                    guideBoolean[6] = 1;
                    Debug.Log("interaction guide clear");
                    typingEffect.StartNarration(narrationSet[6] + 1, narrationSet[7]);
                }
            }
            else if (guideBoolean[7] == 0)
            {
                if (Input.GetKey(KeyCode.I))
                {
                    typingEffect.Skip();
                    guideBoolean[7] = 1;
                    Debug.Log("inventory guide clear");
                    typingEffect.StartNarration(narrationSet[7] + 1, narrationSet[8]);
                }
            }
            else
            {            
                gameObject.SetActive(false);
            }
        }
    }
}
