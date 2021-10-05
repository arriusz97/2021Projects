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
            typingEffect.StartNarration(0, 1);
        }
    }
}
