using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ProjectileHero : MonoBehaviour
{
    private Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision colld)
    {
        // Delete when it either hits a enemy or wall
        if (colld.gameObject.CompareTag("Obstacles"))
        {
            Destroy(this.gameObject);
        }
    }

    public Vector3 vel
    {
        get { return rigid.velocity; }
        set { rigid.velocity = value; }
    }
}
