using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yachtCtrl : MonoBehaviour
{
    [Header("yacht 속성")]
    [SerializeField]
    private float m_speed;
    [SerializeField]
    private yachtDrivingSit m_drivingSitCtrl;
    public playerCtrl_tutorial m_playerCtrl;
    Rigidbody m_rigidbody;

    [Header("Camera 속성")]
    [SerializeField]
    private Camera m_MainCamera;
    [SerializeField]
    private Camera m_YachtCamera;
    public Transform m_cameraArm;
    private float m_lookSensitivity = 2f;
    private float m_cameraRotationLimit = 25f;
    private float m_currentCameraRotationX;

    //Main camera는 값이 0보다 작거나 같을 때 켜지게 하고, yacht camera는 값이 0.2f보다 크거나 같을 때 켜지게 함
    private float cameraTimeSpace_Main = 0.0f;
    private float cameraTimeSpace_yacht;



    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        //player가 driving sit에 앉아있고 cameraTimeSpace_Main값이 0보다 작다면
        if (m_playerCtrl.m_isSit && cameraTimeSpace_Main <= 0.0f)
        {
            float moveDirZ = Input.GetAxisRaw("Horizontal");
            float moveDirX = Input.GetAxisRaw("Vertical");

            Vector3 moveHorizontal = transform.right * -moveDirX;
            Vector3 moveVertical = transform.forward * -moveDirZ;
            Vector3 m_velocity = (moveHorizontal - moveVertical) * m_speed;

            m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);

            //main camera는 꺼두고, yacht 운전석 camera만 켜두기
            m_MainCamera.gameObject.SetActive(false);
            m_YachtCamera.gameObject.SetActive(true);

            cameraTimeSpace_yacht += Time.deltaTime;
            cameraTimeSpace_Main = 0.2f;

            yacht_Rotation();
            camera_Rotation();
        }
        else if(cameraTimeSpace_yacht >= 0.2f)   //player가 driving sit에 앉아있지 않고 cameraTimeSpace가 0.2f를 넘었다면
        {
            //main camera가 켜져있지 않다면
            if (!m_MainCamera.gameObject.activeInHierarchy)
            {
                m_MainCamera.gameObject.SetActive(true);
                m_YachtCamera.gameObject.SetActive(false);
                cameraTimeSpace_yacht = 0.0f; //main camera 켜준 뒤 cameraTimeSpace 초기화
                cameraTimeSpace_Main -= Time.deltaTime;
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
