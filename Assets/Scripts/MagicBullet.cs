using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public float speed = 10f; 
    public float damage = 20f; 

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            if (!PlayerCtrl.Instance.isInvincible)
            {
                PlayerCtrl.Instance.PlayerHP -= (int)damage;
            }

            Destroy(gameObject); 
        }
        else if (hit.gameObject.CompareTag("Untagged") || hit.gameObject.name.Contains("Cube"))
        {
            Destroy(gameObject);
        }
    }
}