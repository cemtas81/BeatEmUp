using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MonkMove : MonoBehaviour
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
    public Button j;
    public bool canjump;
    public GameObject attack;
    public GameObject attackleft;
    public float axisy;
    [Range(0,1)]
    [SerializeField] float smoothing = 0.2f;
    private Vector3 velocity2 = Vector3.zero;
    public void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        pose = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        //j.onClick.AddListener(Jump);
    }
    private void Update()
    {
       
        if (transform.position.y <= axisy)
        {
            OnLanding();
        }
       
    }
    public void Jump()
    {
        if (onjump == false )
        {
            onjump = true;
            rigid.gravityScale = 25f;
            //rigid.AddForce(new Vector2(transform.position.x + 10f, lowjumpm * Time.deltaTime));
            Vector2 movement = new Vector2(rigid.velocity.x, lowjumpm * Time.deltaTime);
            rigid.velocity = movement;
            ani.SetBool("jump", true);
            axisy = transform.position.y;
            //rigid.velocity += Vector2.up * lowjumpm * Time.deltaTime;
            Debug.Log("zıpla");
           
           
        }
        
    }
    
    public void OnLanding()
    {
        onjump = false;
        rigid.gravityScale = 0;
        axisy = transform.position.y;
        ani.SetBool("jump", false);
        Debug.Log("in");
    }

    public void Fire()
    {
        if (onjump==false&&shot==false)
        {
            ani.SetInteger("attackindex", Random.Range(0, 2));
            ani.SetTrigger("shoot");
            shot = true;
            if (pose.flipX==true)
            {
                attackleft.SetActive(true);
            }
            if (pose.flipX==false)
            {
                attack.SetActive(true);
            }
            
        }
         if (onjump == true&&shot==false)
        {
            ani.SetTrigger("flying kick");
            if (pose.flipX == true)
            {
                attackleft.SetActive(true);
            }
            if (pose.flipX == false)
            {
                attack.SetActive(true);
            }
        }
        //else if (v ==-1&&shot==false)
        //{
        //    ani.SetTrigger("shoot");
        //    if (pose.flipX == true)
        //    {
        //        attackleft.SetActive(true);
        //    }
        //    if (pose.flipX == false)
        //    {
        //        attack.SetActive(true);
        //    }
        //}
        
        StartCoroutine(Reshoot());
       
    }
    IEnumerator Reshoot()
    {
        
        yield return new WaitForSeconds(1.5f);
        shot =false;
        attack.SetActive(false);
        attackleft.SetActive(false);
    }
   

    public void FixedUpdate()
    {
       
        if (shot == false )
        {
            h = Mathf.Round(joy.Horizontal);
            v = Mathf.Round(joy.Vertical);

            //Vector3 targvelocity = new Vector2(h * speed * Time.deltaTime, v/2 * speed * Time.deltaTime);
            //rigid.velocity = Vector3.SmoothDamp(rigid.velocity, targvelocity,ref velocity2 ,smoothing);
            Vector3 targvelocity = new Vector2(h * speed * Time.deltaTime, v / 2 * speed * Time.deltaTime);
            Vector2 movement = Vector3.SmoothDamp(rigid.velocity, targvelocity, ref velocity2, smoothing);
            rigid.velocity = movement;
            ani.SetFloat("speed", rigid.velocity.magnitude);
        }
       
        if (h < 0)
        {
            pose.flipX = true;

        }
        else if (h > 0)
        {
            pose.flipX = false;
            
        }
        //if (v == -1)
        //{
        //    ani.SetBool("duck", true);
        //}
        //else if (v == 1)
        //{

        //    Jump();
        //    ani.SetFloat("speed", 0);
        //}
        //else if (v == 0)
        //{
        //    ani.SetBool("duck", false);
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Fire();
        }
      

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.tag == "death")
        {
            SceneManager.LoadScene(0);
        }
       
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ani.SetBool("jump", false);
        if (collision.gameObject.tag == "hurt")
        {
            ani.SetTrigger("hurt");
            Debug.Log("hurt");

        }
        if (collision.gameObject.tag == "hurt")
        {
            ani.SetTrigger("hurt");
            Debug.Log("hurt");

        }
    }
    

   
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hurt")
        {
            ani.ResetTrigger("hurt");
            Debug.Log("unhurt");

        }
    }
}
