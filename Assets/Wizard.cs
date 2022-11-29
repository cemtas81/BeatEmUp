using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public WizardEnemy enemyPunk;
    private Animator ani;
    public GameObject efect;
    
    private void Awake()
    {

        enemyPunk = GetComponentInParent<WizardEnemy>();
        ani = GetComponentInParent<Animator>();
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Projectile")
        {
            efect.SetActive(true);
            enemyPunk.dead = true;
            ani.SetBool("fire", false);
           
        }
    }
}
