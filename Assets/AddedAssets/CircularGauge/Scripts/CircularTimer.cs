using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public enum eCountType { countUp, countDown, HP, TP, O2 }
public enum eFillDirection { fillUp, fillDown }
public enum eFillType { tick, smooth }


public class CircularTimer : MonoBehaviour
{
    public float duration;

    [System.Serializable]
    public class FillSettings
    {
        public Color color;
        public eFillType fillType;
        public Image fillImage;
        public eFillDirection fillDirection;
        public bool capEnabled;
        public Image headCapImage;
        public Image tailCapImage;
    }
    public FillSettings fillSettings;

    [System.Serializable]
    public class BackgroundSettings
    {
        public bool enabled;
        public Color color;
        public Image backgroundImage;
    }
    public BackgroundSettings backgroundSettings;

    [System.Serializable]
    public class TextSettings
    {
        public bool enabled;
        public bool millisecond;
        public TextMeshProUGUI text;
        public Color color;
        public eCountType countType;
        
    }
    public TextSettings textSettings;

    public UnityEvent didFinishedTimerTime;  
    
    float AfterImageTime;

    public bool isPaused = true;
    
    public float CurrentTime { get; set; }

    [SerializeField]
    private float currentTIme;

    private void Start()     //인스펙터의 초기설정 불러오기
    {
        fillSettings.fillImage.color = fillSettings.color;
        fillSettings.headCapImage.color = fillSettings.color;
        fillSettings.tailCapImage.color = fillSettings.color;

        textSettings.text.color = textSettings.color;

        if (backgroundSettings.enabled)
        {
            backgroundSettings.backgroundImage.gameObject.SetActive(true);
            backgroundSettings.backgroundImage.color = backgroundSettings.color;
        }
        else
        {
            backgroundSettings.backgroundImage.gameObject.SetActive(false);
        }

        if (fillSettings.capEnabled)
        {
            showCapImage(true);

            if (fillSettings.fillImage.fillAmount == 0f)
            {
                showCapImage(false);
            }
            else
            {
                showCapImage(true);
            }          
        }
        else
        {
            showCapImage(false);
        }

        if (textSettings.enabled)
        {
            textSettings.text.gameObject.SetActive(true);           
        }
        else
        {
            textSettings.text.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (!isPaused)
        {
            CurrentTime += Time.deltaTime;  //시간 변화 감지
            currentTIme = CurrentTime;

            if (CurrentTime >= duration)    //정해진 시간에 도달하면 타이머를 멈추고 세팅된 이벤트를 호출한다
            {
                isPaused = true;
                CurrentTime = duration;
                didFinishedTimerTime.Invoke();
            }

            switch (fillSettings.fillDirection) //시간 변화에 따른 타이머 게이지 변화
            {
                case eFillDirection.fillDown:

                    if (fillSettings.fillType == eFillType.smooth)
                    {
                        fillSettings.fillImage.fillAmount = CurrentTime / duration;
                    }
                    else if (fillSettings.fillType == eFillType.tick)
                    {
                        fillSettings.fillImage.fillAmount = (float)System.Math.Round(CurrentTime / duration, 1);
                    }
                    break;
                case eFillDirection.fillUp:
                    if (fillSettings.fillType == eFillType.smooth)
                    {
                        fillSettings.fillImage.fillAmount = (duration - CurrentTime) / duration;
                    }
                    else if (fillSettings.fillType == eFillType.tick)
                    {
                        fillSettings.fillImage.fillAmount = (float)System.Math.Round((duration - CurrentTime) / duration, 1);
                    }
                    break;
            }

            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        if (fillSettings.capEnabled)       //캡 설정이 활성화된 경우 캡을 게이지 변화에 맞춰서 이동시킨다
        {           
            Vector3 capRotaionValue = Vector3.zero;
            capRotaionValue.z = 360 * (1 - fillSettings.fillImage.fillAmount);
            fillSettings.tailCapImage.rectTransform.localRotation = Quaternion.Euler(capRotaionValue);
        }

        if (textSettings.enabled)       //텍스트 설정이 활성화된 경우 타이머의 종류 및 시간 변화에 따라 글자를 출력한다
        {
            float time = CurrentTime;

            switch (textSettings.countType)
            {
                case eCountType.HP:
                    textSettings.text.text = "HP";
                    break;
                case eCountType.TP:
                    textSettings.text.text = "TP";
                    break;
                case eCountType.O2:
                    textSettings.text.text = "O<sub>2</sub>";
                    break;
                case eCountType.countUp:
                    if (textSettings.millisecond)
                    {
                        textSettings.text.text = time.ToString("F2");
                    }
                    else
                    {
                        textSettings.text.text = time.ToString("F0");
                    }
                    break;
                case eCountType.countDown:
                    if (textSettings.millisecond)
                    {
                        textSettings.text.text = (duration - time).ToString("F2");
                    }
                    else
                    {
                        textSettings.text.text = (duration - time).ToString("F0");
                    }
                    break;
            }
        }
    }

    void showCapImage(bool isShow)  //캡 이미지를 활성화한다
    {
        fillSettings.headCapImage.enabled = isShow;
        fillSettings.tailCapImage.enabled = isShow;
    }

    public void PauseTimer()    //타이머를 일시정지한다
    {
        isPaused = true;
    }

    public void StartTimer()    //타이머를 시작한다
    {
        isPaused = false;
    }

    public void StopTimer()     //타이머를 정지한 후 리셋한다
    {
        CurrentTime = 0;
        AfterImageTime = 0;
        isPaused = true;
        ResetTimer();
    }

    void ResetTimer()       //타이머를 리셋한다
    {
        switch (fillSettings.fillDirection)
        {
            case eFillDirection.fillDown:
                fillSettings.fillImage.fillAmount = 0;
                break;
            case eFillDirection.fillUp:
                fillSettings.fillImage.fillAmount = 1;
                break;
        }
    }

    public void Activated(bool isActivate)
    {
        gameObject.SetActive(isActivate);
    }
}
