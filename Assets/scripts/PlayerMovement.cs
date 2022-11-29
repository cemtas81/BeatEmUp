using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid;
    public float speed;
    public DynamicJoystick joy;
    public float h;
    public float v;
    private SpriteRenderer pose;
    private Animator ani;
    public bool onjump;
    public float fallm;
    public float lowjumpm;
    private bool shot;
    public Projectile ProjectilePrefab;
    public Transform LaunchOffset;
    public Projectile ProjectilePrefab2;
    public Transform LaunchOffset2;
    public bool canjump;
    
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        pose = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }
    public void Jump()
    {
        if (onjump == false&&shot==false&&canjump==true)
        {
            //rigid.velocity += Vector2.up * lowjumpm * Time.deltaTime;
            Debug.Log("zıpla");
            ani.SetBool("jump", true);
            //rigid.AddForce(Vector2.up * Time.deltaTime * lowjumpm, ForceMode2D.Impulse);
            Vector2 movement = new Vector2(rigid.velocity.x, lowjumpm * Time.deltaTime);
            rigid.velocity = movement;
            canjump = false;
            onjump = true;
        }

    }
   
    public void Fire()
    {
        if (onjump==false&&shot==false)
        {
            ani.SetTrigger("shoot");
            shot = true;
            StartCoroutine(refire());
        }
       
    }
   
    IEnumerator refire()
    {
        yield return new WaitForSeconds(0.25f);
        if (pose.flipX == true)
        {
            Instantiate(ProjectilePrefab2, LaunchOffset2.position, LaunchOffset2.rotation);
        }
        else
        Instantiate(ProjectilePrefab,LaunchOffset.position, LaunchOffset.rotation);

        yield return new WaitForSeconds(0.5f);
       
        shot = false;
    }

    public void FixedUpdate()
    {
        if (shot==false&&v>=0)
        {
            h = joy.Horizontal;
           
            Vector2 movement =new Vector2( Mathf.Round(h) * speed * Time.deltaTime,rigid.velocity.y);
            rigid.velocity = movement;
            ani.SetFloat("speed", Mathf.Ceil(Mathf.Abs(h)));
        }
        v = Mathf.Round(joy.Vertical);
        if (h < 0)
        {
            pose.flipX = true;
            ani.SetBool("duck", false);
        }
        else if (h > 0)
        {
            pose.flipX = false;
            ani.SetBool("duck", false);
        }
        if (v == -1)
        {
            ani.SetBool("duck", true);
        }
        else if (v == 1)
        {
           
            //Jump();
            ani.SetBool("duck", false);
        }
        else if (v==0)
        {
            ani.SetBool("duck", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (rigid.velocity.y<=0)
        {
            ani.SetBool("jump", false);
            Fall();
        }
        
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "platform")
        {
            onjump = false;
            rigid.gravityScale = 1;
            //StartCoroutine(Fall());
            canjump = true;
        }
        if (collision.gameObject.tag == "death")
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            onjump = true;
            ani.SetBool("jump", true);
            rigid.gravityScale = fallm;
           
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
        
    //    if (collision.gameObject.tag == "platform")
    //    {
    //        ani.SetBool("jump", false);
    //        onjump = false;
    //        rigid.gravityScale = 1;
    //        //StartCoroutine(Fall());
    //    }
    //}

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(1);
        canjump = true;
    }
}
