using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private bool canMove = true;
    [Tooltip(("If your character does not jump, ignore all below 'Jumping Character'"))]
    [SerializeField] private bool doesCharacterJump = false;

    [Header("Base / Root")]
    [SerializeField] private Rigidbody2D baseRB;
    [SerializeField] private float hSpeed = 10f;
    [SerializeField] private float vSpeed = 6f;
    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;

    [Header("'Jumping' Character")]
    [SerializeField] private Rigidbody2D charRB;
    [SerializeField] private float jumpVal = 10f;
    [SerializeField] private int possibleJumps = 1;
    [SerializeField] private int currentJumps = 0;
    [SerializeField] private bool onBase = false;
    [SerializeField] private Transform jumpDetector;
    [SerializeField] private float detectionDistance;
    [SerializeField] private LayerMask detectLayer;
    [SerializeField] private float jumpingGravityScale;
    [SerializeField] private float fallingGravityScale;
    private bool jump;
    private Animator ani;
    private bool facingRight = true;
    public SpriteRenderer sprite;
    public GameObject spirte2;
    private Vector3 velocity = Vector3.zero;
    private bool shot;
    public bool death;
    public GameObject attack;
    public GameObject attackleft;
    public bool flyingkick;
    PlayerInput input;
    Controls controls = new Controls();
    public GameObject deathpic;
    // Start is called before the first frame update
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        ani = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        controls = input.GetInput();
        if (controls.JumpState && currentJumps < possibleJumps)
        {
            jump = true;
           
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Fire();
        }
        if (death==true)
        {
            canMove = false;
            baseRB.velocity = Vector2.zero;
            charRB.velocity = Vector2.zero;
            deathpic.SetActive(true);
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
    public void Jumping()
    {
        if (currentJumps < possibleJumps)
        {
            jump = true;
        }
       
    }
    public void Fire()
    {
        if (onBase && !shot)
        {
            ani.SetInteger("attackindex", Random.Range(0,2));
            ani.SetTrigger("shoot");
            shot = true;
           
            Debug.Log("punch");
            canMove = false;
            baseRB.velocity = Vector2.zero;
            charRB.velocity = Vector2.zero;
            StartCoroutine(Reshoot());
        }
        if (!onBase && !shot)
        {
            ani.SetTrigger("flying kick");
            shot = true;
            Debug.Log("kick");
            StartCoroutine(Reshoot2());
        }
  
    }
    IEnumerator Reshoot2()
    {
       
        yield return new WaitForSeconds(0.4f);
        if (sprite.flipX == true)
        {
            attackleft.SetActive(true);
        }
        if (sprite.flipX == false)
        {
            attack.SetActive(true);
        }

        yield return new WaitForSeconds(0.3f);
        shot = false;
        attack.SetActive(false);
        attackleft.SetActive(false);
    
    }
   
    IEnumerator Reshoot()

    {
        yield return new WaitForSeconds(0.15f);
        if (sprite.flipX == true)
        {
            attackleft.SetActive(true);
        }
        if (sprite.flipX == false)
        {
            attack.SetActive(true);
        }


        yield return new WaitForSeconds(0.3f);
        shot = false;
        attack.SetActive(false);
        attackleft.SetActive(false);
        canMove = true;
   
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!onBase && doesCharacterJump)
        {
            detectBase();
            
        }

        if (canMove)
        {
           
            Vector3 targetVelocity = new Vector2(controls.HorizontalMove * hSpeed, controls.VerticalMove * vSpeed);
            Vector2 _velocity = Vector3.SmoothDamp(baseRB.velocity, targetVelocity, ref velocity, movementSmooth);
            baseRB.velocity = _velocity;
            ani.SetFloat("speed", baseRB.velocity.magnitude);
            if (transform.position.y>=-3)
            {
                transform.position = new Vector2(transform.position.x,- 3);
            }
            if (transform.position.y <= -7.3)
            {
                transform.position = new Vector2(transform.position.x, -7.3f);
            }
            //----- 
            if (doesCharacterJump)
            {
                if (onBase)
                {
                    // on base
                    charRB.velocity = _velocity;
                    //ani.ResetTrigger("flying kick");
                    ani.SetBool("jump", false);
                    ani.SetBool("falling", false);
                }
                else
                {
                    // in air
                    if (charRB.velocity.y <= 0)
                    {
                        charRB.gravityScale = fallingGravityScale;
                        ani.SetBool("falling",true);
                        ani.SetBool("jump", false);
                    }
                    
                    charRB.velocity = new Vector2(_velocity.x, charRB.velocity.y);
                    ani.SetBool("falling", false);
                }

                if (jump)
                {
                    charRB.AddForce(Vector2.up * jumpVal, ForceMode2D.Impulse);
                    charRB.gravityScale = jumpingGravityScale;
                    jump = false;
                    currentJumps++;
                    onBase = false;
                    ani.SetBool("jump", true);
                    
                }
            }
           
            if (controls.HorizontalMove > 0  )
            {
                
                sprite.flipX = false;
            } else if(controls.HorizontalMove < 0 )
            {
               
                sprite.flipX = true;
            }
        }
    }

    private void detectBase()
    {

        RaycastHit2D hit = Physics2D.Raycast(jumpDetector.position, -Vector2.up, detectionDistance, detectLayer);
        if(hit.collider != null)
        {
            onBase = true;
            currentJumps = 0;
            
        }
    }

    private void OnDrawGizmos()
    {
        if (doesCharacterJump)
        {
            Gizmos.DrawRay(jumpDetector.transform.position, -Vector3.up * detectionDistance);
           
        }
    }
   
}
