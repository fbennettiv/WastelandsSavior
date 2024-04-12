using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

using UnityEngine;

public class HeroController : MonoBehaviour, IWeapon
{
    public enum eMode { idle, walk, shoot, die }

    // Implementing IWeapon
    public enum eWeapon { none, pistol, m_gun, knife }

    [Header("Inscribed")]
    public float speed = 30;
    public float lookSpeed = 1;
    private Rigidbody rigid;
    private Camera camMain;
    private WeaponController weaponCon;
    private bool shoot;

    [Header("Dynamic")]
    private Vector3 _forwardPos;
    public Vector3 forwardPos
    {
        get { return _forwardPos; }
        set { _forwardPos = value; }
    }
    private Vector3 mouseDir;
    private Animator anim;
    public eMode mode;
    public eWeapon weapon;
    [HideInInspector]
    public Quaternion rotation;
    [HideInInspector]
    public PickUp pickUp;
    public bool isWeapon = false;
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

    public delegate void WeaponFireDelegate();
    public event WeaponFireDelegate fireEvent;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        weaponCon = GameObject.Find("torso").GetComponent<WeaponController>();
        mode = eMode.idle;
        weapon = eWeapon.none;
        mouseDir = new Vector3();
        _forwardPos = new Vector3();

        camMain = Camera.main;
        _health = _maxHealth;
    }

    private void Update()
    {
        AnimationController();

        if (death) { return; }
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
        _forwardPos = new Vector3(mouseDir.x, 0, mouseDir.z) - tXZ;
        rotation = Quaternion.LookRotation(_forwardPos, transform.up);
        Quaternion rotationSlerp = Quaternion.Slerp(transform.rotation, rotation, lookSpeed);

        // Change transform.position based on the axis
        Vector3 pos = transform.position;
        pos.x += hAxis * speed * Time.deltaTime;
        pos.z += vAxis * speed * Time.deltaTime;
        // Move and rotate the Hero
        rigid.Move(pos, rotationSlerp);
    }

    void AnimationController()
    {
        // Shooting Trigger
        shoot = Input.GetMouseButton(0);

        // Movement/Death modes
        if (death) { mode = eMode.die; }
        else if (shoot)
        {
            mode = eMode.shoot;
            death = false;
        }
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
        eMode previousMode = mode;
        switch (mode)
        {
            case eMode.idle:
                anim.Play("idle", 0);
                // anim.Play("holding-both-shoot", 0);
                anim.speed = 1;
                break;
            case eMode.walk:
                anim.Play("walk", 0);
                anim.speed = 1;
                break;
            case eMode.shoot:
                // Play animation based on what weapon is active

                switch (weapon)
                {
                    case eWeapon.none:
                        mode = previousMode;
                        break;
                    case eWeapon.knife:
                        if (weaponCon.LActive)
                        {
                            anim.Play("attack-melee-left", 0);
                            anim.speed = 1;
                        }
                        else if (weaponCon.RActive)
                        {
                            anim.Play("attack-melee-right", 0);
                            anim.speed = 1;
                        }
                        break;
                    case eWeapon.pistol:
                        if (weaponCon.RActive)
                        {
                            anim.Play("holding-both-shoot", 0);
                            anim.speed = 1;
                            fireEvent();
                        }
                        else if (weaponCon.LActive)
                        {
                            anim.Play("holding-left-shoot", 0);
                            anim.speed = 1;
                            fireEvent();
                        }
                        break;
                    /*case eWeapon.m_gun:
                        if (weaponCon.LActive)
                        {
                            anim.Play("holding-both-shoot", 0);
                            anim.speed = 1;
                        }
                        else if (weaponCon.RActive)
                        {
                            anim.Play("holding-right-shoot", 0);
                            anim.speed = 1;
                        }
                        break;*/
                }
                break;
            case eMode.die:
                death = true;
                anim.Play("die", 0);
                break;
        }
    }

    void OnTriggerEnter(Collider trigger)
    {
        pickUp = trigger.gameObject.GetComponent<PickUp>();
        isWeapon = false;

        if (pickUp == null) { Debug.LogError("PickUp was null (HeroController"); return; }
        if (pickUp.item == PickUp.eType.none) return;

        switch (pickUp.item)
        {
            case PickUp.eType.weapon:
                isWeapon = true;
                break;
            /*case PickUp.eType.health:
                break;
            case PickUp.eType.gem:
                break;*/
            default:
                Debug.LogError("No PickUp item" + pickUp.item);
                break;
        }
    }
}
