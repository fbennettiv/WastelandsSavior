using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum eType { none, health, gem, weapon }
    public enum eWeapon { none, pistol, m_gun, knife }

    [Header("Inscribed")]
    public eType item;
    public eWeapon weaponType;

    private Collider collide;

    private void Start()
    {
        collide = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
            Destroy(this.gameObject);
    }
}
