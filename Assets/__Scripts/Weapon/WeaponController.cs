using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private GameObject pistolR,pistolL, sword, m_gun;
    private HeroController hero;

    void Start()
    {
        // Finding the weapons Transform components
        Transform pistolRight = transform.Find("pistol_Left");
        Transform pistolLeft = transform.Find("pistol_Right");

        if (pistolRight != null)
        {
            Debug.LogError("Could not find pistol_Right child of arm_right");
            return;
        }
        pistolR = pistolRight.gameObject;
        if (pistolLeft != null)
        {
            Debug.LogError("Could not find pistol_Left child of arm_Left");
            return;
        }
        pistolL = pistolLeft.gameObject;

        // Finding the Hero component
        hero = GetComponentInParent<HeroController>();
        if (hero == null) 
        {
            Debug.LogError("Could not find parent component Dray.");
            return;
        }
        pistolL.SetActive(false);
        pistolL.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pistolL.SetActive(hero.mode == HeroController.eMode.shoot && hero.weapon == HeroController.eWeapon.pistolL);
        pistolR.SetActive(hero.mode == HeroController.eMode.shoot && hero.weapon == HeroController.eWeapon.pistolR);

    }
}
