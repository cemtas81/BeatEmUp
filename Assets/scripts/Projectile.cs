using System.Collections;

using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    public float speed = 5f;
    private void Awake()
    {
        StartCoroutine(Destroy1());
    }
    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        
    }
   
    
    IEnumerator Destroy1()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            Destroy(gameObject);
        }
       
    }
}
