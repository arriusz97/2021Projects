using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlGuide : MonoBehaviour
{
    [SerializeField]
    private TypingEffect typingEffect;

    [SerializeField]
    private int[] narrationSet = new int[10];

    public int[] guideBoolean = Enumerable.Repeat<int>(0, 10).ToArray<int>();

    // Start is called before the first frame update
    void Start()
    {
        if (guideBoolean[0] == 0)
        {
            typingEffect.StartNarration(0, narrationSet[0]);
        }
        else if(guideBoolean[9] == 1)
        {
            gameObject.SetActive(false);
        }
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
                    typingEffect.StartNarration(narrationSet[0]+1, narrationSet[1]);
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
        }
    }
}
