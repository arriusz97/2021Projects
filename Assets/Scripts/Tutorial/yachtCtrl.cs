using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterSystem.Data;

public class yachtCtrl : MonoBehaviour
{
    [Header("yacht 속성")]
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private yachtDrivingSit m_drivingSitCtrl;
    public playerCtrl_tutorial m_playerCtrl;
    Rigidbody m_rigidbody;
    [SerializeField]
    float tempRotate = 1.5f;

    [Header("Camera 속성")]
    [SerializeField]
    private Camera m_MainCamera;
    [SerializeField]
    private Camera m_YachtCamera;
    public Transform m_cameraArm;
    private float m_lookSensitivity = 1f;
    private float m_cameraRotationLimit = 10f;
    private float m_currentCameraRotationX;

    [Header("Sound")]
    [SerializeField]
    private AudioSource m_waterSound;
    [SerializeField]
    private AudioSource m_YachtDrivingSound;
    [SerializeField]
    private AudioSource m_YachtStopSound;
    [SerializeField]
    private AudioSource m_StormSound;

    [Header("Rain")]
    [SerializeField]
    private GameObject m_YachtCamera_Rain;
    [SerializeField]
    private StormTrigger m_StormTrigger;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_waterSound.Play();
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

                WaterSurfaceData m_water = new WaterSurfaceData();
                BasicWaves _basicWaveSettings = new BasicWaves(10f, 30f, 5f);

                if (!m_StormSound.isPlaying)
                {
                    m_StormSound.Play();
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
