using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public DateTime time;
    public bool collided = false;
    
    void Awake()
    {
        time = System.DateTime.Now;
        GameManager.Instance.RegisterProjectile(this);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!collided)
        {
            collided = true;
            GameManager.Instance.UnregisterProjectile(this);
            Destroy(gameObject, 0.01f);
        }
    }
}
