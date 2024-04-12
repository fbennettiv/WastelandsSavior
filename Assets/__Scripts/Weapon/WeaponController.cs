using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour, IWeapon
{
    private enum eWeapon { none, pistol, m_gun, knife }

    private GameObject pistolR, pistolL, sword, m_gun, torso;
    private HeroController hero;
    private PickUp pickUp;

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
        if (!hero.isWeapon) { return; }
        pickUp = hero.pickUp;

        // Problems
        /*-----------------------------------------------*/
        /* make sure knife has appropriate adjustments due to animations
         * make sure a pistol cannot be instantiated if there is an active L or R or B */

        if (pickUp.weaponType == PickUp.eWeapon.none) return;

        if (RActive && LActive) return;
        switch (pickUp.weaponType)
        {
            case PickUp.eWeapon.pistol:
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                    pickUp.weaponType = PickUp.eWeapon.none;
                }
                else if (!pistolL.activeInHierarchy)
                {
                    LActive = true;
                    pistolL.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                    pickUp.weaponType = PickUp.eWeapon.none;
                }
                break;

            // Yet to be implemented
            /*case "knife":
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.knife;
                    pickUp.weaponType = PickUp.eWeapon.none;
                }
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.knife;
                    pickUp.weaponType = PickUp.eWeapon.none;
                }
                break;*/

            /*
            case "machinegun":
                if (!pistolR.activeInHierarchy)
                {
                    RActive = true;
                    pistolR.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                    pickUp.weaponType = PickUp.eWeapon.none;
                }
                else if(!pistolL.activeInHierarchy)
                {
                    LActive = true;
                    pistolL.SetActive(true);
                    hero.weapon = HeroController.eWeapon.pistol;
                    pickUp.weaponType = PickUp.eWeapon.none;
                }
                else if(pistolR.activeInHierarchy && pistolL.activeInHierarchy)
                {
                    BActive = true;
                    hero.weapon = HeroController.eWeapon.pistol;
                    pickUp.weaponType = PickUp.eWeapon.none;
                }
                break;*/
            default:
                hero.weapon = HeroController.eWeapon.none;
                Debug.LogError("No hero weapon" + pickUp.name);
                break;
        }
    }
}
