using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunk : MonoBehaviour
{
    private GameObject target;
    private Collider2D player;
    private Transform targ;
    public float speed;
    private bool yakinda;
    private Animator ani;
    public bool dead;
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    public float mindistance;
    public GameObject hurt;
    public GameObject hurtright;
   
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
        if (target==null)
        {
            Debug.Log("Yok");
        }
        if (target!=null)
        {
            if (yakinda == true && dead == false )
            {

                targ = target.GetComponent<Transform>();
                if (Vector2.Distance(targ.position, transform.position) > Mathf.Abs(mindistance))
                {
                    Vector2 dir = (targ.position - transform.position).normalized;
                    transform.position = Vector2.MoveTowards(transform.position, targ.position, speed * Time.deltaTime);
                    ani.SetFloat("speed", 1);
                    ani.SetBool("punch", false);
                }
                if (Vector2.Distance(targ.position, transform.position) <= Mathf.Abs(mindistance))
                {
                    //rigid.velocity = Vector2.zero;
                    ani.SetFloat("speed", 0);
                    ani.SetBool("punch", true);
                    //transform.Translate(Vector2.zero);
                    StartCoroutine(Repunch());

                }

                if (transform.position.x < targ.transform.position.x)
                {
                    sprite.flipX = true;
                }
                else
                    sprite.flipX = false;

            }
            if (dead == true || Camera.main.transform.position.x - 20 >= transform.position.x)
            {
                ani.SetBool("punch", false);
                ani.SetTrigger("death");
                //rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                //rigid.velocity = Vector2.zero;

                transform.Translate(Vector2.zero);
                hurt.SetActive(false);
                hurtright.SetActive(false);
                Destroy(gameObject, 2);
            }
            if (yakinda == false && dead == false)
            {

                ani.SetFloat("speed", 0);
                ani.SetBool("punch", false);
                //transform.Translate(Vector2.zero);
            }
            if (transform.position.y >= -2)
            {
                transform.position = new Vector2(transform.position.x, -2);
            }
            if (transform.position.y <= -7.3)
            {
                transform.position = new Vector2(transform.position.x, -7.3f);
            }
        }
       
        

    }
    IEnumerator Repunch()
    {
       
            yield return new WaitForSeconds(0.4f);

            if (sprite.flipX == true)
            {
                hurtright.SetActive(true);

            }
            if (sprite.flipX == false)
            {
                hurt.SetActive(true);

            }
            yield return new WaitForSeconds(0.4f);
            hurt.SetActive(false);
            hurtright.SetActive(false);
   

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
           
            yakinda = false;
            //ani.SetFloat("speed", 0);
        }
    }
    
   

}
