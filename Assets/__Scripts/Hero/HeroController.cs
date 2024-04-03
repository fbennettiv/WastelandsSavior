using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 30;
    private Rigidbody rigid;

    //[Header("Dynamic")]

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        /*rigid.rotation = Quaternion.Euler(0, 90, -90);
        rigid.constraints = RigidbodyConstraints.FreezeRotationY;
        rigid.constraints = RigidbodyConstraints.FreezeRotationZ;*/
    }

    private void Update()
    {
        // Pull in information from Input Class
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        // Change transform.position based on the axis
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.y += vAxis * speed * Time.deltaTime;
        rigid.transform.position = pos;
        rigid.transform.rotation = Quaternion.Euler(vAxis,90,-90);
        //rigid.transform.SetPositionAndRotation(pos, Quaternion.Euler(0,pos.x,0));
    }
}
