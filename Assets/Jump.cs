using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float lowjumpm;
    public void Jumpy()
    {
        rigid.velocity += Vector2.up * lowjumpm * Time.deltaTime;
        Debug.Log("iiii");
    }
}
