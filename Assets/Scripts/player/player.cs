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
    public bool m_isAttack = false;

    Rigidbody m_rigidbody;
    CapsuleCollider m_collider;
    Animator m_Anim;
    [SerializeField]
    ActionController m_actionController;

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
    private float m_cameraRotationLimit = 50f;
    private float m_currentCameraRotationX;
    [SerializeField]
    private Animator m_cameraAnimator;

    [Header("playerHP변수")]
    [SerializeField]
    private float m_maxHP;
    [SerializeField]
    private float m_currentHP;

    [Header("Knife 변수")]
    [SerializeField]
    private GameObject m_knife;

    [Header("Player UI")]
    [SerializeField]
    private GameObject m_playerBloodUI;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_collider = GetComponent<CapsuleCollider>();
        m_Anim = GetComponent<Animator>();

        m_currentHP = m_maxHP;
        
    }

    //enemy에게 맞았을 때 불릴 함수
    public void Hit(float damage)
    {
        if (!m_isDead)
        {
            //To do:
            //enemy한테 맞았다면 blood UI 켜지고 2초 뒤에 꺼지게 하기
            m_playerBloodUI.SetActive(true);

            //만약에 blood UI가 켜졌다면 코루틴 시작 -> 2초 뒤에 꺼지게
            if (m_playerBloodUI.activeInHierarchy)
            {
                StartCoroutine(BloodUI());
            }
            else
            {
                StopCoroutine(BloodUI());
            }
            m_currentHP -= damage;
        }

        if (m_currentHP <= 0)
        {
            m_isDead = true;
            m_Anim.SetTrigger("DEAD");

        }
    }

    IEnumerator BloodUI()
    {
        WaitForSeconds two = new WaitForSeconds(2);
        yield return two;

        m_playerBloodUI.SetActive(false);
    }

    //enemy를 공격할 때 불릴 함수
    public void AttackTarget(GameObject target)
    {
        target.SendMessage("Hit", 40);
    }


    // Update is called once per frame
    void Update()
    {

        if (!m_actionController.playerLock && !m_isDead)
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


            if (m_JumpCount < 1 && Input.GetButtonDown("Jump") && !m_SwimTrigger.m_isWater)
            {
                m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, 5, m_rigidbody.velocity.z);
                m_JumpCount++;
            }
            m_Anim.SetFloat("JUMP", m_rigidbody.velocity.y);


            //Run & Dive -> left shift
            if (Input.GetKeyDown(KeyCode.LeftShift))
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

            //Diveup -> left control
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

            //줍기 -> Z
            if (Input.GetKey(KeyCode.Z))
            {
                m_Anim.SetBool("PICKUP", true);
            }
            else
            {
                m_Anim.SetBool("PICKUP", false);
            }

            //Attack -> mouse left button
            if (Input.GetMouseButtonDown(0))
            {
                m_isAttack = true;
                //물 속이 아니라면
                if (!m_SwimTrigger.m_isWater)
                {
                    m_Anim.SetBool("ATTACK", true);
                    m_Anim.SetBool("IDLE", false);
                }
                else  //물 속이라면
                {
                    m_Anim.SetBool("ATTACK", true);
                    m_Anim.SetBool("IDLEINWATER", false);
                    m_Anim.SetBool("SWIM", false);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                m_isAttack = false;
                //물 속이 아니라면
                if (!m_SwimTrigger.m_isWater)
                {
                    m_Anim.SetBool("ATTACK", false);
                    m_Anim.SetBool("IDLE", true);
                }
                else //물속이라면
                {
                    m_Anim.SetBool("ATTACK", false);
                    m_Anim.SetBool("IDLEINWATER", true);
                }
            }
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
        m_Anim.SetBool("IDLE", false);
        m_rigidbody.useGravity = false;

        m_cameraAnimator.SetBool("Camera_Idle", true);
        m_cameraAnimator.SetBool("Camera_Walk", false);
        m_cameraAnimator.SetBool("Camera_Run", false);

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
            }
            else if (m_isDiveup & this.transform.position.y < -20)
            {
                var vel = m_rigidbody.velocity;
                vel.y = +5f;
                m_rigidbody.velocity = vel;
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
            var vel = m_rigidbody.velocity;
            vel.y = 0f;
            vel.x = 0f;
            m_rigidbody.velocity = vel;
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
            m_cameraAnimator.SetBool("Camera_Idle", false);
        }
        else
        {
            isMove = false;
            m_Anim.SetBool("IDLE", true);
            m_cameraAnimator.SetBool("Camera_Idle", true);
            m_cameraAnimator.SetBool("Camera_Walk", false);
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
                m_cameraAnimator.SetBool("Camera_Run", false);
                if (isMove)
                {
                    m_Anim.SetBool("WALK", true);
                    m_cameraAnimator.SetBool("Camera_Walk", true);
                }
                else
                {
                    m_Anim.SetBool("IDLE", true);
                    m_cameraAnimator.SetBool("Camera_Walk", false);
                }
                m_rigidbody.MovePosition(transform.position + m_velocity * Time.deltaTime);
            }
            else
            {
                m_Anim.SetBool("RUN", true);
                m_Anim.SetBool("WALK", false);
                m_cameraAnimator.SetBool("Camera_Run", true);
                m_cameraAnimator.SetBool("Camera_Walk", false);
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
