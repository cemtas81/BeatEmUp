using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : MonoBehaviour
{
    public EnemyPunk enemyPunk;
    private Animator ani;
    public int health;
    public GameObject efect;
    //public bool Dead;
    private Collider2D coll;
    private void Awake()
    {

        enemyPunk = GetComponentInParent<EnemyPunk>();
        ani = GetComponentInParent<Animator>();
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Projectile")
        {
            
            ani.SetBool("hurt",true);
            
            ani.SetFloat("speed", 0);
            health -= 1;
            efect.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Projectile")
        {
            efect.SetActive(false);
            ani.SetBool("hurt", false);
            if (health<=0)
            {
                enemyPunk.dead = true;
                coll.enabled = false;
            }

        }
    }


  
}
