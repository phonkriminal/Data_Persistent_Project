using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    public UnityEvent<Brick> onChange;
    public int PointValue;

    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 10 :
                block.SetColor("_BaseColor", Color.green);
                break;
            case 20:
                block.SetColor("_BaseColor", Color.yellow);
                break;
            case 50:
                block.SetColor("_BaseColor", Color.blue);
                break;
            default:
                block.SetColor("_BaseColor", Color.red);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        onDestroyed.Invoke(PointValue);
        onChange.Invoke(this);
        
        //slight delay to be sure the ball have time to bounce
        Destroy(gameObject, 0.1f);
    }
}
