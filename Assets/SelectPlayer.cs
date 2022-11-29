using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectPlayer : MonoBehaviour
{
   
    public Animator ani1;
    public Animator ani2;
    public Animator ani3;
    public void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                if (raycastHit.collider.CompareTag("Finish"))
                {
                    PlayerPrefs.SetInt("player", 1);
                    ani1.SetTrigger("select");
                    StartCoroutine(level());

                }
                if (raycastHit.collider.CompareTag("Respawn"))
                {
                    PlayerPrefs.SetInt("player", 2);
                    ani2.SetTrigger("select");
                    StartCoroutine(level());
                }

                if (raycastHit.collider.CompareTag("Player"))
                {
                    PlayerPrefs.SetInt("player", 3);
                    ani3.SetTrigger("select");
                    StartCoroutine(level());
                }
            }
        }
    }
    private IEnumerator level()
    {
      
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
       

    }
}
