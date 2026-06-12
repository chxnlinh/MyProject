using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxInteraction : MonoBehaviour
{

    public PlayerCtrl _playerCtrl; 

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        _playerCtrl = player.GetComponent<PlayerCtrl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.name == "PlayerSword" && _playerCtrl.PlayerAttking )
        {
            Destroy(gameObject); 
        }        
    }




}
