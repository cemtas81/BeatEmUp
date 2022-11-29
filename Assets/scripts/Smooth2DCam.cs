using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth2DCam : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float xoffset;
    public float yoffset;
    private void Awake()
    {
        //target = GameObject.FindWithTag("Player").transform;
        StartCoroutine(Find());
    }
    IEnumerator Find()
    {
        yield return new WaitForSeconds(0.1f);
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); 
            Vector3 destination = transform.position + delta;
            //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(destination.x, destination.y + yoffset,-10), ref velocity, dampTime);
        }

    }
   
}