using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour, IWeapon
{
    public enum eWeapon { none, pistol, m_gun, knife }

    private GameObject pistolR, pistolL, sword, m_gun, torso;
    private HeroController hero;

    public bool LActive;
    public bool RActive;
    public bool BActive;

    void Start()
    {
        // Finding the weapons Transform components
        pistolL = GameObject.Find("pistol_Left");
        pistolR = GameObject.Find("pistol_Right");

        if (pistolR == null)
        {
            Debug.LogError("Could not find pistol_Right child of arm_right");
            return;
        }
        if (pistolL == null)
        {
            Debug.LogError("Could not find pistol_Left child of arm_Left");
            return;
        }

        // Finding the Hero component
        hero = GetComponentInParent<HeroController>();
        if (hero == null)
        {
            Debug.LogError("Could not find parent component Dray.");
            return;
        }
        pistolL.SetActive(false);
        pistolR.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //pistolL.SetActive(hero.mode == HeroController.eMode.shoot && hero.weapon == HeroController.eWeapon.pistolL);
        //pistolR.SetActive(hero.mode == HeroController.eMode.shoot && hero.weapon == HeroController.eWeapon.pistolR);

        //pistolL.SetActive(hero.mode == HeroController.eMode.shoot);
        //pistolR.SetActive(hero.mode == HeroController.eMode.shoot);

        if (hero.isWeapon) { return; }
        GameObject weapon = hero.itemPickUp;

        // DO NOT RETRIVE GAMEOBJECT (or find alternative to current problem)
        // check if the item has a "weapon tag" like in PickUp
        // Fix communication between the herocontroller and weaponController
        // hardcoding names is sloppy, dont do that shit
        if (!weapon.CompareTag("Weapon")) return;

        if (BActive) return;
        switch (weapon.name)
        {
            /*case "knife":
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.knife;
                }
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.knife;
                }
                break;*/
            case "pistol":
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                }
                else if (!pistolL.activeInHierarchy)
                {
                    LActive = true;
                    pistolL.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                }
                else if (pistolR.activeInHierarchy && pistolL.activeInHierarchy)
                {
                    BActive = true;
                    hero.weapon = HeroController.eWeapon.pistol;
                }
                break;
            /*
            case "machinegun":
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                }
                else if(!pistolL.activeInHierarchy)
                {
                    LActive = true;
                    pistolL.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                }
                else if(pistolR.activeInHierarchy && pistolL.activeInHierarchy)
                {
                    BActive = true;
                    hero.weapon = HeroController.eWeapon.pistol;
                }
                break;*/
            default:
                hero.weapon = HeroController.eWeapon.none;
                Debug.LogError("No hero weapon" +  weapon.name);
                break;
        }
    }
}
