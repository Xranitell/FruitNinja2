using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    void Start()
    {
        Vector2 spriteSize = GetComponent<SpriteRenderer>().sprite.bounds.size;

        var camera = Camera.main;
        
        var height = camera.orthographicSize;
        var width = height * camera.aspect;
        var size = width * 2f / spriteSize.x;
        
        transform.localScale = new Vector3(size, size, 1f) * 1.2f;
        transform.position = new Vector3(0f, 0f, 0f);
    }

}
