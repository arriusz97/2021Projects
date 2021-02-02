using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeunController : MonoBehaviour
{
    [SerializeField]
    private GameObject mMeunBackground, mMeunButton, mBackButton;

    private bool mMeunActivated;
    // Start is called before the first frame update
    void Start()
    {
        mMeunActivated = false;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (mMeunActivated)
                onBackButtonDown();
            else
                onMeunButtonDown();
        }   
    }

    public void onMeunButtonDown()
    {
        mMeunBackground.gameObject.SetActive(true);
        mBackButton.gameObject.SetActive(true);
        mMeunButton.gameObject.SetActive(false);
        mMeunActivated = true;
    }

    public void onBackButtonDown()
    {
        mMeunBackground.gameObject.SetActive(false);
        mMeunButton.gameObject.SetActive(true);
        mBackButton.gameObject.SetActive(false);
        mMeunActivated = false;
    }
}
