using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class HeroController : MonoBehaviour, IWeapon
{
    public enum eMode { idle, walk, shoot, die }

    // Implementing IWeapon
    public enum eWeapon { none, pistolR, pistolL, m_gunR, m_gunL, knifeR, knifeL }

    [Header("Inscribed")]
    public float speed = 30;
    public float lookSpeed = 1;
    private Rigidbody rigid;
    private Camera camMain;
    private bool shoot;

    [Header("Dynamic")]
    private Vector3 mouseDir;
    private Animator anim;
    private GameObject torso;
    public eMode mode;
    public eWeapon weapon;
    [SerializeField]
    private float _health = 0;
    public float health
    {
        get { return _health; }
        set { _health = value; }
    }
    private float _maxHealth = 10;
    private float _score = 0;
    public bool death = true;
    // Axis for directional input
    private float hAxis = 0;
    private float vAxis = 0;
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        torso = GameObject.Find("torso");
        mouseDir = new Vector3();
        camMain = Camera.main;
        _health = _maxHealth;
        mode = eMode.idle;
        weapon = eWeapon.none;
    }

    private void Update()
    {
        AnimationController();

        if (death) { return; }


        shoot = Input.GetMouseButtonDown(0);
        if (shoot) { mode = eMode.shoot; }
    }

    private void FixedUpdate()
    {
        if (death) { return; }

        MovementController();
    }

    void MovementController()
    {
        // Pull in information from Input Class
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");

        // Mouse Rotation
        mouseDir = camMain.ScreenToWorldPoint(Input.mousePosition);
        Vector3 tXZ = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 relativePos = new Vector3(mouseDir.x, 0, mouseDir.z) - tXZ;
        Quaternion rotation = Quaternion.LookRotation(relativePos, transform.up);
        rotation = Quaternion.Slerp(transform.rotation, rotation, lookSpeed);

        // Change transform.position based on the axis
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.z += vAxis * speed * Time.deltaTime;
        // Move and rotate the Hero
        rigid.Move(pos, rotation);
    }

    void AnimationController()
    {
        // Movement/Death modes
        if (death) { mode = eMode.die; }
        else if (hAxis != 0 || vAxis != 0)
        {
            mode = eMode.walk;
            death = false;
        }
        else
        {
            mode = eMode.idle;
            death = false;
        }

        // Modes for animations
        switch (mode)
        {
            case eMode.idle:
                anim.Play("idle", 0);
                anim.speed = 1;
                break;
            case eMode.walk:
                anim.Play("walk", 0);
                anim.speed = 1;
                break;
            case eMode.shoot:
                // Get the child weapon from hands
                break;
            case eMode.die:
                death = true;
                anim.Play("die", 0);
                break;
        }
    }
}
