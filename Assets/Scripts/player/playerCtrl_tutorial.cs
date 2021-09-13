﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCtrl_tutorial : MonoBehaviour
{
    [Header("player변수")]
    [SerializeField]
    float m_speed;

    [SerializeField]
    Vector3 m_dir;

    private int m_JumpCount = 0;
    public bool m_isRun;

    public bool isMove = false;

    Rigidbody m_rigidbody;
    CapsuleCollider m_collider;
    Animator m_Anim;

    //서있을 때 킬 collider, 앉아있을 때 킬 collider 따로 만들어서 관리하기
    [SerializeField]
    private CapsuleCollider m_upperBodyCollider;
    [SerializeField]
    private CapsuleCollider m_lowerBodyCollider;

    [Header("camera변수")]
    public Camera m_camera;
    public Transform m_cameraArm;
    //public GameCtrl m_gameCtrl;
    private float m_lookSensitivity = 2f;
    private float m_cameraRotationLimit = 50f;
    private float m_currentCameraRotationX;


    [Header("yacht driving")]
    [SerializeField]
    private yachtDrivingSit m_drivingSitCtrl;  //bool 변수로 trigger check
    [SerializeField]
    private GameObject m_yachtDrivingSit;
    public bool m_isSit = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<CapsuleCollider>();
        m_Anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //앉은 상태면 움직이지 X
        if (!m_isSit)
        {
            Move();
            camera_Rotation();
            character_Rotation();
        }

        if (m_JumpCount < 1 && Input.GetButtonDown("Jump")) // && !m_SwimTrigger.m_isWater
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 6, m_rigidbody.velocity.z);
            m_JumpCount++;

        }
        m_Anim.SetFloat("JUMP", m_rigidbody.velocity.y);


        //player가 sit zone에 들어오고, 상호작용 키 F 를 눌렀다면
        if (m_drivingSitCtrl.m_playerEnter && Input.GetKey(KeyCode.F) && !m_isSit)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;  //긴 원형 collider off
            m_upperBodyCollider.gameObject.SetActive(true);
            m_lowerBodyCollider.gameObject.SetActive(true);
            m_Anim.SetBool("SIT", true);
            m_Anim.SetBool("IDLE", false);
            m_isSit = true;
            playerDriving();
        }

    }

    void playerDriving()
    {
        if (m_isSit)
        {
            transform.position = new Vector3(-15f, -15.5f, 2f);
            transform.rotation = Quaternion.Euler(0f, -90f, 0f);
            m_rigidbody.isKinematic = true;  //yacht가 움직일 때 player가 움직이지 않게 is kinematic을 켜줌
            this.transform.SetParent(m_yachtDrivingSit.transform);
        }
    }

    void character_Rotation()
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

        m_camera.transform.localEulerAngles = new Vector3(m_currentCameraRotationX, 0, 0);
    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * -moveDirZ;
        Vector3 m_velocity = (moveHorizontal - moveVertical) * m_speed;

        if (!m_rigidbody.useGravity)
        {
            m_rigidbody.useGravity = true;
        }


        if (moveDirX != 0 || moveDirZ != 0)
        {
            isMove = true;
            m_Anim.SetBool("IDLE", false);
        }
        else
        {
            isMove = false;
            if (!m_isSit)
            {
                m_Anim.SetBool("IDLE", true);
            }
        }


        if (isMove)
        {
            m_Anim.SetBool("WALK", true);
            m_Anim.SetBool("IDLE", false);
        }
        else
        {
            if (!m_isSit)
            {
                m_Anim.SetBool("IDLE", true);
                m_Anim.SetBool("WALK", false);
            }
        }
        m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Yacht"))
        {
            m_JumpCount = 0;
        }
    }
}