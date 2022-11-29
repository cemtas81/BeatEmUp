using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxMultiplier;
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureSizex;
    [SerializeField] private float mult;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite= GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureSizex = mult*texture.width / sprite.pixelsPerUnit;
       
    }
    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position +=new Vector3( deltaMovement.x*parallaxMultiplier.x,deltaMovement.y * parallaxMultiplier.y);
        lastCameraPosition = cameraTransform.position;

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureSizex)
        {
            float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureSizex;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
        
    }
}
