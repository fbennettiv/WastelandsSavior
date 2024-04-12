using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ItemDefinition
{
    [Tooltip("Color of Item")]
    public Color itemColor = Color.white;

    [Tooltip("Prefab of Weapon model")]
    public GameObject weaponModelPrefab;

    [Tooltip("Prefab of projectile that is fired")]
    public GameObject projectilePrefab;

    [Tooltip("Color of the projectile that is fired")]
    public Color projectileColor = Color.white;

    [Tooltip("Damage caused when a single projectile hits an enemy")]
    public float damageOnHit = 0;

    [Tooltip("Seconds to delay between shots")]
    public float delayBetweenShots = 0;

    [Tooltip("Velocity of individual projectiles")]
    public float velocity = 30;
}

public class Gun : MonoBehaviour, IWeapon
{
    private enum eWeapon { none, pistol, m_gun, knife }
    private Transform projectileAnchor;

    [Header("Dynamic")]
    private HeroController hero;
    public ItemDefinition itemDef;
    private Transform bulletPointTrans;
    private WeaponController weaponCon;
    public float nextShotTime;

    // Start is called before the first frame update
    void Start()
    {
        if (projectileAnchor == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            projectileAnchor = go.transform;
        }

        hero = GameObject.Find("Hero").GetComponent<HeroController>();
        weaponCon = GameObject.Find("torso").GetComponent<WeaponController>();
        bulletPointTrans = GameObject.Find("BulletPoint").transform;
        if (hero != null) { hero.fireEvent += Fire; }
    }

    private void Fire()
    {
        if (!gameObject.activeInHierarchy) { return; }

        if (Time.time < nextShotTime) return;

        ProjectileHero pHero;
       Vector3 vel = hero.forwardPos.normalized * itemDef.velocity;

        switch (hero.weapon)
        {
            case HeroController.eWeapon.pistol:
                pHero = MakeProjectile();
                pHero.vel = vel;
                break;
        
            case HeroController.eWeapon.m_gun:
            //pHero = MakeProjectile(projectileAnchor);
            //pHero.vel = vel;
            break;
            /*case HeroController.eWeapon.shotgun:
                  p = MakeProjectile();
                  p.vel = vel;
                  p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                  p.vel = p.transform.rotation * vel;
                  p = MakeProjectile();
                  p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                  p.vel = p.transform.rotation * vel;
                  break;*/
        }
    }

    private ProjectileHero MakeProjectile()
    {
        GameObject gObj;
        
        bulletPointTrans = this.transform.Find("BulletPoint").transform;
        nextShotTime = Time.time + itemDef.delayBetweenShots;

        gObj = Instantiate(itemDef.projectilePrefab, bulletPointTrans.position, 
            new Quaternion(0,hero.rotation.y,0,1) * Quaternion.AngleAxis(90, Vector3.right),projectileAnchor);
        ProjectileHero pHero = gObj.GetComponent<ProjectileHero>();

        return pHero;
    }
}
