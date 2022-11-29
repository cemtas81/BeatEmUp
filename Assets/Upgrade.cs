using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    private SpriteRenderer sprite;
    private SpriteRenderer sprite2;
    private Transform target;
    public GameObject life;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sprite2 = GameObject.FindGameObjectWithTag("barrel").GetComponent<SpriteRenderer>();
        StartCoroutine(Find());
    }
    
IEnumerator Find()
{
    yield return new WaitForSeconds(0.1f);
    target = GameObject.FindWithTag("Player").GetComponent<Transform>();
   
}

private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Projectile")
        {
            if (target.position.x>=transform.position.x)
            {
                sprite.flipX = true;
            }
            
            sprite.sprite = sprite2.sprite;
          
            transform.position = new Vector2(transform.position.x, transform.position.y - 1f);
            GetComponent<Collider2D>().enabled = false;
            life.SetActive(true);
            Destroy(gameObject, 1);
        }
    }
   
}
