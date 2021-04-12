using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [Header("player변수")]
    [SerializeField]
    float m_speed;
    [SerializeField]
    float m_runSpeed;
    [SerializeField]
    Vector3 m_dir;
    public SwimTrigger m_SwimTrigger;
    public bool m_isDead = false;
    private int m_JumpCount = 0;
    private bool m_isRun;

    Rigidbody m_rigidbody;
    CapsuleCollider m_collider;
    Animator m_Anim;

    [Header("Swim 변수")]
    [SerializeField]
    private float m_SwimSpeed;
    public bool m_isDive = false;
    private bool m_isDiveup = false;

    [Header("camera변수")]
    public Camera m_camera;
    public Transform m_cameraArm;
    //public GameCtrl m_gameCtrl;
    private float m_lookSensitivity = 2f;
    private float m_cameraRotationLimit = 40f;
    private float m_currentCameraRotationX;

    [Header("playerHP변수")]
    [SerializeField]
    private float m_maxHP;
    private float m_currentHP;



    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<CapsuleCollider>();
        m_Anim = GetComponent<Animator>();

        m_currentHP = m_maxHP;
        
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        camera_Rotation();
        character_Rotation();

        if (!m_SwimTrigger.m_isWater)
        {
            Move();
        }
        else
        {
            Swim();
        }


        if (m_JumpCount < 1 && Input.GetButtonDown("Jump"))
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 5, m_rigidbody.velocity.z);
            m_JumpCount++;
        }
        m_Anim.SetFloat("JUMP", m_rigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            if (!m_SwimTrigger.m_isWater)
            {
                m_isRun = true;
            }
            else
            {
                m_isDive = true;
            }
        }


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_isDive = false;
            m_isRun = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (m_SwimTrigger.m_isWater)
            {
                m_isDiveup = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_isDiveup = false;
        }


        if (Input.GetKey(KeyCode.Z))
        {
            m_Anim.SetBool("PICKUP", true);
        }
        else
        {
            m_Anim.SetBool("PICKUP", false);
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

    private void Swim()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * -moveDirZ;
        Vector3 m_velocity = (moveHorizontal - moveVertical) * m_SwimSpeed;

        m_Anim.SetBool("WALK", false);
        m_Anim.SetBool("RUN", false);
        m_rigidbody.useGravity = false;

        if (moveDirX != 0 || moveDirZ != 0)
        {
            m_Anim.SetBool("SWIM", true);
            m_Anim.SetBool("IDLEINWATER", false);

            m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);
            if (m_isDive)
            {
                var vel = m_rigidbody.velocity;
                vel.y = -5f;
                m_rigidbody.velocity = vel;
                Debug.Log("is Dive");
            }
            else if (m_isDiveup & this.transform.position.y < -20)
            {
                var vel = m_rigidbody.velocity;
                vel.y = +5f;
                m_rigidbody.velocity = vel;
                Debug.Log("is Dive up" + this.transform.position.y);
            }
            else
            {
                var vel = m_rigidbody.velocity;
                vel.y = 0f;
                m_rigidbody.velocity = vel;
            }
        }
        else
        {
            m_Anim.SetBool("IDLEINWATER", true);
        }

    }

    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * -moveDirZ;
        Vector3 m_velocity = (moveHorizontal - moveVertical) * m_speed;

        bool isMove = false;
        m_Anim.SetBool("SWIM", false);

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
            m_Anim.SetBool("IDLE", true);
        }

        if (!m_SwimTrigger.m_isWater)
        {
            m_Anim.SetBool("WALK", isMove);
        }

        if (isMove && !m_SwimTrigger.m_isWater)
        {
            m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);

            if (!m_isRun)
            {
                m_Anim.SetBool("RUN", false);
                if (isMove)
                {
                    m_Anim.SetBool("WALK", true);
                }
                else
                {
                    m_Anim.SetBool("IDLE", true);
                }
                m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);
            }
            else
            {
                m_Anim.SetBool("RUN", true);
                m_Anim.SetBool("WALK", false);
                m_rigidbody.MovePosition(transform.position + (moveHorizontal - moveVertical) * m_runSpeed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_JumpCount = 0;
        }
    }
}
