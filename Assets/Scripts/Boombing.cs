using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boombing : MonoBehaviour
{
    public float explosionRadius = 5f;
    public int explosionTime = 3;
    public int boombHuntNum = 200;
    SphereCollider BoombCollider;
    public GameObject ExplosionFX;
    bool isExplosion = false; 


    void Start()
    {
        BoombCollider = GetComponent<SphereCollider>();
        Invoke("BoombExplosion", explosionTime); 
    }


    void Update()
    {
       

    }

    void BoombExplosion()
    {
        Instantiate(ExplosionFX, transform.position, transform.rotation);
        isExplosion = true; 
        for (float r = 0.001f; r < explosionRadius; r += 0.2f)
            BoombCollider.radius = r;
        Destroy(gameObject, 1); 
    }
    private void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.tag== "Enemy" && isExplosion)
        {
            NPCctrl npc = hit.gameObject.GetComponent<NPCctrl>();
            npc.NPC_HP = npc.NPC_HP - boombHuntNum;
            isExplosion = false; 
        }

    }


}
