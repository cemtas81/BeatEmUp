using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardEnemy : MonoBehaviour
{
    private GameObject target;
    private Collider2D player;
    private Transform targ;
    
    private bool yakinda;
    private Animator ani;
    public bool dead;
    private SpriteRenderer sprite;
    public Projectile ProjectilePrefab;
    public Transform LaunchOffset;
    public Projectile ProjectilePrefab2;
    public Transform LaunchOffset2;
   
    
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
       
        ani = GetComponent<Animator>();
        StartCoroutine(Find());
    }
    IEnumerator Find()
    {
        yield return new WaitForSeconds(0.1f);
        target = GameObject.FindWithTag("Player");
        player = target.GetComponent<Collider2D>();
    }
    void Update()
    {

        if (yakinda == true && dead == false)
        {
            
            targ = target.GetComponent<Transform>();

            ani.SetBool("fire", true);

            StartCoroutine(Repunch());
            if (transform.position.x < targ.transform.position.x)
            {
                sprite.flipX = true;

            }
            else
                sprite.flipX = false;
        }
        else if (dead == true||Camera.main.transform.position.x - 20 >= transform.position.x)
        {
            ani.SetBool("fire", false);
            ani.SetTrigger("death");
            transform.Translate(Vector2.zero);
            Destroy(gameObject, 2);
        }
       
        else
            ani.SetBool("fire", false);


    }
    IEnumerator Repunch()
    {
        yield return new WaitForSeconds(2f);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            yakinda = true;
            InvokeRepeating("Launch", 1.6f, 2f);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            yakinda = false;
            CancelInvoke();
        }
    }
    private void Launch()
    {
        if (sprite.flipX == true)
        {
            Instantiate(ProjectilePrefab2, LaunchOffset2.position, LaunchOffset2.rotation);
        }
        else
            Instantiate(ProjectilePrefab, LaunchOffset.position, LaunchOffset.rotation);
    }

    

}
