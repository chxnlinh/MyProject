using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI; 

public class NPCctrl : MonoBehaviour
{

    NavMeshAgent NPCnav;
    Animator NPCani; 

    public Transform Target;

    float NPCwalkSpeed;

    public static bool NPCisAttking = false;

    public int NPC_maxHP = 100;
    public int DetcDist = 10; 
    public int NPC_HP;
    public Image NPC_HPimg;

    public PlayerCtrl _playerCtrl;

    public GameObject CameraObj;
    Transform NPChpCanvas;

    public GameObject reward;
    bool isReward = true;


    void Start()
    {
        NPCnav = GetComponent<NavMeshAgent>();
        NPCani = GetComponent<Animator>(); 

        GameObject player = GameObject.Find("Player");
        Target = player.transform;


        Transform NPC_HPbar = transform.Find("NPC_Canvas/NPC_HP");
        NPC_HPimg = NPC_HPbar.GetComponent<Image>();

           NPC_HP = NPC_maxHP;

        //GameObject player = GameObject.Find("Player");
        _playerCtrl = player.GetComponent<PlayerCtrl>();

        CameraObj = GameObject.Find("Main Camera");
        NPChpCanvas = transform.Find("NPC_Canvas"); 
    }


    void Update()
    {
        NPChpCanvas.LookAt(CameraObj.transform.position);

       // NPC_HPimg.fillAmount = NPC_HP * 0.01f;
        NPC_HPimg.fillAmount = (float)NPC_HP / (float)NPC_maxHP; 

        NPCwalkSpeed = NPCnav.velocity.magnitude;
        NPCani.SetFloat("NPCwspeed", NPCwalkSpeed); 

        float NPCplayerDistance = Vector3.Distance(transform.position, Target.position);

      

        if(NPCplayerDistance < DetcDist && NPC_HP > 0)
        {
            NPCnav.destination = Target.position;

            if(NPCplayerDistance < 1.75f && _playerCtrl.PlayerHP > 0)
            {
                Vector3 TargetPos = Target.position;
                TargetPos.y = transform.position.y;                
                transform.LookAt(TargetPos); 

                NPCani.SetBool("NPCpunch", true);

                NPCisAttking = NPCani.GetFloat("NPC-PunchTime") > 0.01f ? true:false ;

            }
            else
            {
                NPCani.SetBool("NPCpunch", false);
            }
        }
        if (NPC_HP <= 0 && isReward)
            {
                NPCani.SetTrigger("NPCdying");

                if (isReward)
                {
                    Instantiate(reward, transform.position, Quaternion.identity);
                    isReward = false; 
                }              

                Destroy(gameObject, 5); 
            }

        
    }

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.name == "PlayerSword" && NPC_HP >0 && _playerCtrl.PlayerAttking )
        {
            NPC_HP -= PlayerCtrl.Instance.SwordHuntNum;        
        }        
    }

}
