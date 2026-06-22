using System.Collections; // [新增] 必須加入這行才能使用協程 (IEnumerator)
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

    // ==========================================
    // [新增] 道具狀態變數
    // ==========================================
    public bool isInvincible = false;   // 是否處於無敵狀態
    public bool isSpeedBoosted = false; // 是否處於加速狀態

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

            // [修改] 如果沒有喝加速飲料，才執行原本的走路/跑步速度邏輯
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

        // [修改] 加上 && !isInvincible，確保無敵狀態下不會扣血
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

    // ==========================================
    // [新增] 能量飲料 (加速) 邏輯
    // ==========================================
    public void DrinkEnergyPotion(float duration)
    {
        StartCoroutine(SpeedBoostRoutine(duration));
    }

    private IEnumerator SpeedBoostRoutine(float duration)
    {
        isSpeedBoosted = true; // 開啟加速狀態，避免被 Update 蓋掉
        speed = 8f;            // 設定超級速度 (可依需求調整數字)
        
        // 等待指定的秒數
        yield return new WaitForSeconds(duration); 
        
        isSpeedBoosted = false; // 時間到，關閉加速狀態 (Update 會自動接管改回 2 或 4)
    }

    // ==========================================
    // [新增] 防護罩 (無敵) 邏輯
    // ==========================================
    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        isInvincible = true; // 開啟無敵狀態
        
        // 💡 如果你之後有做一個「發光防護罩」的 GameObject，可以在這邊把它打開
        // 例如：ShieldEffectObj.SetActive(true);

        yield return new WaitForSeconds(duration);
        
        isInvincible = false; // 時間到，關閉無敵狀態

        // 💡 時間到時把它關掉
        // 例如：ShieldEffectObj.SetActive(false);
    }
}