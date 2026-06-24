using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    private CharacterController PlayerControl;
    public Animator PlayerAniCtrl;
    private Vector3 moveDirection;
    public float speed = 2f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;
    public int RoateSpeed = 80;

    public Image PlayerHPbarImg;
    public int PlayerHP = 100;
    public int MaxPlayerHP = 150;

    public GameObject PlayerSowrd;
    public bool PlayerSwordEnable = false;

    public bool PlayerAttking = false;
    public bool PlayerBoxing = false;

    public GameObject CameraObj;
    Transform PlayerHPcavans;

    public VariableJoystick joyStick;
    public bool VirButSword = false;

    public TrailRenderer SwordTrailR;

    public static PlayerCtrl Instance;

    public int SwordHuntNum = 40;

    public bool isInvincible = false;
    public bool isSpeedBoosted = false;

    public AudioSource swordAudio;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PlayerControl = GetComponent<CharacterController>();
        PlayerAniCtrl = GetComponent<Animator>();

        GameObject PlayerHPbar = GameObject.Find("PlyaerHP");
        PlayerHPbarImg = PlayerHPbar.GetComponent<Image>();

        PlayerSowrd.SetActive(false);

        PlayerHPcavans = transform.Find("PlayerCanvas");
        CameraObj = GameObject.Find("Main Camera");
        joyStick = FindObjectOfType<VariableJoystick>();
        SwordTrailR = PlayerSowrd.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        PlayerHPcavans.LookAt(CameraObj.transform.position);

        PlayerHPbarImg.fillAmount = (float)PlayerHP / (float)MaxPlayerHP;

        float RotateX = Input.GetAxis("Horizontal") + joyStick.Horizontal ;
        transform.Rotate(0,RotateX * Time.deltaTime * RoateSpeed , 0);

        if (PlayerControl.isGrounded || isGrouded() )
        {
            float moveZ = Input.GetAxis("Vertical") + joyStick.Vertical;

            if (!isSpeedBoosted)
            {
                speed = Input.GetKey(KeyCode.LeftShift) ? 4 : 2;
            }

            PlayerAniCtrl.SetFloat("WalkSpeed", Mathf.Abs( moveZ * speed) );

            moveDirection = new Vector3(0f, 0f, moveZ * speed);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                PlayerAniCtrl.SetTrigger("PlayerJump");
                moveDirection.y = jumpForce;
            }

            if (Input.GetKeyUp(KeyCode.B) )
            {
                PlayerAniCtrl.SetTrigger("PlayerBoxing");
            }

            if((Input.GetKeyDown(KeyCode.T) || VirButSword) && PlayerSwordEnable )
            {
                PlayerAniCtrl.SetTrigger("PlayerSwordAttk");
                VirButSword = false;

                if (swordAudio != null)
                {
                    swordAudio.Play();
                }
            }
            PlayerAttking = PlayerAniCtrl.GetFloat("PlayerSwordAttkTiming") > 0.01f ? true : false;
            PlayerBoxing = PlayerAniCtrl.GetFloat("PlayerBoxingTiming") > 0.01f ? true : false;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        PlayerControl.Move(moveDirection * Time.deltaTime);
        SwordTrailRender();

        if (PlayerHP <= 0)
        {
            PlayerAniCtrl.SetTrigger("PlayerDying");
        }
    }

    void SwordTrailRender()
    {
        if (PlayerAttking)
        {
            SwordTrailR.enabled = true;
        }
        else
        {
            SwordTrailR.enabled = false;
        }
    }

    bool isGrouded()
    {
        float groundCheckDistance = 1f;
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.name == "Sword")
        {
            PlayerSwordEnable = true;
            Destroy(hit.gameObject);
            PlayerSowrd.SetActive(true);
        }

        if( hit.gameObject.tag == "NPC_Hands" && PlayerHP > 0 && NPCctrl.NPCisAttking && !isInvincible)
        {
            PlayerHP -= 1;
        }
    }

    public void SwordAttkPressDown()
    {
        VirButSword = true;
    }

    public void SwordAttPressUp()
    {
        VirButSword = false;
    }

    public void Heal(int amount)
    {
        PlayerHP += amount;
        PlayerHP = Mathf.Min(PlayerHP, MaxPlayerHP);
    }

    public void DrinkEnergyPotion(float duration)
    {
        StartCoroutine(SpeedBoostRoutine(duration));
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        isSpeedBoosted = true;
        speed = 8f;

        yield return new WaitForSeconds(duration);

        isSpeedBoosted = false;
    }

    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        isInvincible = true;

        yield return new WaitForSeconds(duration);

        isInvincible = false;
    }
}