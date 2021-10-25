using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem.Data;
using WaterSystem;

public class yachtCtrl : MonoBehaviour
{
    public Water _water;

    [Header("yacht 속성")]
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private yachtDrivingSit m_drivingSitCtrl;
    public playerCtrl_tutorial m_playerCtrl;
    public TutorialManager tutorialManager;
    Rigidbody m_rigidbody;
    [SerializeField]
    float tempRotate = 1.5f;

    [Header("Camera 속성")]
    [SerializeField]
    private Camera m_MainCamera;
    [SerializeField]
    private Camera m_YachtCamera;
    public Transform m_cameraArm;
    private float m_lookSensitivity = 0.8f;
    private float m_cameraRotationLimit = 10f;
    private float m_currentCameraRotationX;

    [Header("Sound")]
    [SerializeField]
    private AudioSource m_waterSound;
    [SerializeField]
    private AudioSource m_YachtDrivingSound;
    [SerializeField]
    private AudioSource m_YachtStopSound;
    public AudioSource m_StormSound;

    [Header("Rain")]
    [SerializeField]
    private GameObject m_YachtCamera_Rain;
    [SerializeField]
    private StormTrigger m_StormTrigger;

    [Header("Thunder")]
    public bool b_IsThunder;    //안내 UI 띄울 bool 변수

    [Header("WallPosition")]
    public float m_leftWall;
    public float m_rightWall;
    public float m_upWall;
    public float m_bottomWall;


    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_waterSound.Play();

        _water.surfaceData._basicWaveSettings.amplitude = 4f;
        _water.surfaceData._basicWaveSettings.wavelength = 50f;
        _water.Init();
    }

    private void Update()
    {
        //player가 driving sit에 앉아있다면
        if (m_drivingSitCtrl.m_playerSit)
        {
            float moveDirZ = Input.GetAxisRaw("Horizontal");
            float moveDirX = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = transform.right * -moveDirX;
            Vector3 moveVertical = transform.forward * -moveDirZ;
            Vector3 m_velocity = (moveHorizontal - moveVertical) * m_speed;

            m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);
           
            yacht_Rotation();
            camera_Rotation();

            //main camera는 꺼두고, yacht 운전석 camera만 켜두기, yacht camera에 붙은 rain 켜주기
            m_MainCamera.gameObject.SetActive(false);

            if (m_StormTrigger.m_Storm_Start)
            {
                m_YachtCamera_Rain.SetActive(true);

                transform.position = new Vector3(Mathf.PingPong(Time.time, 3), transform.position.y, transform.position.z);
                transform.rotation = Quaternion.Euler(Mathf.PingPong(Time.time * tempRotate, 5f), this.transform.rotation.y, 0f);

                if (!m_StormSound.isPlaying)
                {
                    m_StormSound.Play();
                    b_IsThunder = true;
                    //water 높이 바꿔주기
                    _water.surfaceData._basicWaveSettings.amplitude = 20f;
                    _water.surfaceData._basicWaveSettings.wavelength = 100f;

                    _water.Init();

                    Debug.Log("message coroutine 실행");
                    tutorialManager.Message02_func();
                    
                }
                m_waterSound.Stop();
              
            }


            if (moveDirX != 0 || moveDirZ != 0)
            {
                if (!m_YachtDrivingSound.isPlaying)
                {
                    m_YachtDrivingSound.Play();
                }
                m_waterSound.Stop();
                m_YachtStopSound.Stop();
            }
            else
            {
                if (!m_YachtStopSound.isPlaying)
                {
                    m_YachtStopSound.Play();
                }
                m_YachtDrivingSound.Stop();
            }

        }
        else
        {
            m_MainCamera.gameObject.SetActive(true);
            m_YachtCamera_Rain.SetActive(false);
            m_YachtDrivingSound.Stop();
            m_waterSound.Play();
            if (!m_YachtStopSound.isPlaying)
            {
                m_YachtStopSound.Play();
            }
        }

        if(this.transform.position.z >= 270)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 270f);
        }
        else if(this.transform.position.z <= 900)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 900f);
        }
        else if(this.transform.position.x >= -330)
        {
            this.transform.position = new Vector3(-330f, this.transform.position.y, this.transform.position.z);
        }
        else if(this.transform.position.x <= -930)
        {
            this.transform.position = new Vector3(-930f, this.transform.position.y, this.transform.position.z);
        }
    }

    void yacht_Rotation()
    {
        float YRotation = Input.GetAxisRaw("Mouse X");
        Vector3 charRotationY = new Vector3(0, YRotation, 0) * m_lookSensitivity;
        m_rigidbody.MoveRotation(m_rigidbody.rotation * Quaternion.Euler(charRotationY));

    }

    void camera_Rotation()
    {
        float XRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = XRotation * m_lookSensitivity;
        m_currentCameraRotationX -= cameraRotationX;
        m_currentCameraRotationX = Mathf.Clamp(m_currentCameraRotationX, -m_cameraRotationLimit, m_cameraRotationLimit);

        m_YachtCamera.transform.localEulerAngles = new Vector3(m_currentCameraRotationX, -95f, 0);
    }

}
