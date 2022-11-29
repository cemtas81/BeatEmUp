using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private Animator ani;
    public float health;
    public Slider slider;
    public Vector3 offset;
    //public Color high;
    //public Color low;
    public float maxHealth;
    public CharacterMovement pl;
    void Start()
    {
        ani = GetComponentInParent<Animator>();
        slider.maxValue = maxHealth;
        slider.value = health;
        pl = GetComponentInParent<CharacterMovement>();
    }
 
    
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position+offset);
        if (health<=0)
        {
            ani.SetBool("hurt", true);
            pl.death = true;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ani.SetBool("jump", false);
        if (collision.gameObject.tag == "hurt")
        {
            ani.SetTrigger("hurt");
            Debug.Log("hurt");
            health -= 3;
            slider.value = health;
        
        }
        else if (collision.gameObject.tag == "punch")
        {
         
            Debug.Log("hurt");
            health -= 0.5f;
            slider.value = health;
            
           
        }
        else if (collision.gameObject.tag=="life")
        {
            Debug.Log("life+");
            health += 25f;
            slider.value = health;
            Destroy(collision.gameObject);
        }
       
    }
}
