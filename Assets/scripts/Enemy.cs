using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;
    private Collider2D player;
    private Transform targ;
    public float speed;
    private bool yakinda;
    private Animator ani;
    public bool dead;
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindWithTag("Player");
        player = target.GetComponent<Collider2D>();
        ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
      
    }
   

    // Update is called once per frame
    void Update()
    {
       
        if (yakinda==true&&dead==false)
        {
            targ = target.GetComponent<Transform>();
            //transform.position = Vector2.MoveTowards(transform.position,new Vector2(targ.position.x,0), speed * Time.deltaTime);
            Vector2 dir = (new Vector2(targ.position.x,0) -new Vector2 (rigid.transform.position.x,0)).normalized;
            rigid.MovePosition(rigid.position + dir * speed * Time.fixedDeltaTime);
            ani.SetFloat("speed", 1);
            ani.SetFloat("speed", 1);
            if (transform.position.x < targ.transform.position.x)
            {
                sprite.flipX = true;
            }
            else
                sprite.flipX = false;
        }

        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            yakinda = true;

        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.Translate(Vector2.zero);
            yakinda = false;
            ani.SetFloat("speed", 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Projectile")
        {
            dead = true;
            ani.SetTrigger("death");
            StartCoroutine(Dead());
            GetComponent<Collider2D>().enabled = false;
            ani.SetFloat("speed", 0);
        }
    }
  IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.8f);

        Destroy(gameObject);
    }

}
