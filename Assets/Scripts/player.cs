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

        //player가 물에 들어가지 않았다면 move 실행, 물에 들어 갔다면 swim 실행
        if (!m_SwimTrigger.m_isWater)
        {
            Move();
        }
        else
        {
            Swim();
        }

        // double jump 막기 위해 jump count가 1 이하일 때만 jump 되도록, 물에서 jump 되는 것 막기 위해
        if (m_JumpCount < 1 && Input.GetButtonDown("Jump") && !m_SwimTrigger.m_isWater)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 5, m_rigidbody.velocity.z);
            m_JumpCount++;
        }
        m_Anim.SetFloat("JUMP", m_rigidbody.velocity.y);


        //player가 물에 있지 않고, left shift 눌렀다면 run true
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


        //left shift key에서 손 뗐다면 dive, run false
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_isDive = false;
            m_isRun = false;
        }

        //만약 left control key 누르고, 물속에 있다면 dive up true
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (m_SwimTrigger.m_isWater)
            {
                m_isDiveup = true;
            }
        }

        //left control key에서 손 뗐다면 dive up false
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            m_isDiveup = false;
        }

        //z key 누르면 pick up true
        if (Input.GetKey(KeyCode.Z))
        {
            m_Anim.SetBool("PICKUP", true);
        }
        else
        {
            m_Anim.SetBool("PICKUP", false);
        }

        //마우스 왼쪽 버튼 누르면 attack true
        if (Input.GetMouseButton(0))
        {
            m_Anim.SetBool("ATTACK", true);
        }
        else
        {
            m_Anim.SetBool("ATTACK", false);
        }


    }

    // camera rotation 참조 -> https://wergia.tistory.com/230
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

    
    //swim 함수
    private void Swim()
    {
        //horizontal, vertical 입력받기
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * -moveDirZ;
        Vector3 m_velocity = (moveHorizontal - moveVertical) * m_SwimSpeed;

        m_Anim.SetBool("WALK", false);
        m_Anim.SetBool("RUN", false);
        m_Anim.SetBool("IDLE", false);

        //물 속에서는 gravity false
        m_rigidbody.useGravity = false;

        //horizontal, vertical 입력이 0이 아니라면
        if (moveDirX != 0 || moveDirZ != 0)
        {
            //swim 실행
            m_Anim.SetBool("SWIM", true);
            m_Anim.SetBool("IDLEINWATER", false);

            m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);
            //만약 dive가 true라면 -> left shift key를 눌렀다면
            if (m_isDive)
            {
                //아래로 내려가도록 rigidbody velocity -5
                var vel = m_rigidbody.velocity;
                vel.y = -5f;
                m_rigidbody.velocity = vel;
            }
            else if (m_isDiveup & this.transform.position.y < -20)
            {
                //만약 left control key 누르고, player의 위치가 -20보다 아래라면 (바다속에 있다면)
                //위로 올라가도록 rigidbody velocity +5
                var vel = m_rigidbody.velocity;
                vel.y = +5f;
                m_rigidbody.velocity = vel;
            }
            else
            {
                //그 외의는 player의 y축 움직이지 않도록 0으로 조정
                var vel = m_rigidbody.velocity;
                vel.y = 0f;
                m_rigidbody.velocity = vel;
            }
        }
        else
        {
            m_Anim.SetBool("IDLEINWATER", true);
            var vel = m_rigidbody.velocity;
            vel.y = 0f;
            m_rigidbody.velocity = vel;
        }

    }

    //move 함수
    private void Move()
    {
        //horizontal, vertical 입력받기
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * -moveDirZ;
        Vector3 m_velocity = (moveHorizontal - moveVertical) * m_speed;

        bool isMove = false;
        m_Anim.SetBool("SWIM", false);

        //gravity가 꺼져있다면 켜주기
        if (!m_rigidbody.useGravity)
        {
            m_rigidbody.useGravity = true;
        }

        //horizontal, vertical 입력값이 0이 아니라면 is move -> true
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

        //isMove가 true고 물속에 있지 않다면
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
