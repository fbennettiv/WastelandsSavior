using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PickUp : MonoBehaviour
{
    public enum eType { none, health, gem, weapon}

    [Header("Inscribed")]
    public eType item;

    private Collider collide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hero"))
            Destroy(this.gameObject);
    }
}
