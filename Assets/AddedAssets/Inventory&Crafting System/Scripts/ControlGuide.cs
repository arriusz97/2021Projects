using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlGuide : MonoBehaviour
{
    [SerializeField]
    private TypingEffect typingEffect;

    public int[] guideBoolean = Enumerable.Repeat<int>(0, 10).ToArray<int>();

    // Start is called before the first frame update
    void Start()
    {
        if (guideBoolean[0] == 0)
        {
            typingEffect.StartNarration(0, 2);
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
                    typingEffect.StartNarration(3, 4);
                }
            }
            else if (guideBoolean[1] == 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    typingEffect.Skip();
                    guideBoolean[1] = 1;
                    Debug.Log("run guide clear");                   
                    typingEffect.StartNarration(5, 5);
                }
            }
        }
    }
}
