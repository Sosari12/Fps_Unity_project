using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Camera cam;
    public LayerMask groundLayer;
    public LayerMask ShootableLayers;
    private Vector3 destination;
    [HideInInspector]
    public Transform hitPoint;
    


    [Header("ShootingValues")]
    [HideInInspector] public float timeToFire;
    private float maxTimeToFire;
    [HideInInspector] public bool readyToFire;
    [HideInInspector] public float projectileSpeed;
    [HideInInspector] public float damage;
    [HideInInspector] public float penetration;
    [HideInInspector] public float power;
    [HideInInspector] public bool projectileWeapon;
    [HideInInspector] public bool shootWithAnim;

    [Header("Objects")]
    public GameObject projectilePrefab;
    public Transform gunPoint;
    [HideInInspector] public Animator weaponAnim;
    public GameObject hitVFX;

    [Header("Debug")]
    private RaycastHit debugHit;
    private LineRenderer laserLine;



    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        maxTimeToFire = timeToFire;
    }

    // Update is called once per frame
    void Update()
    {
        if (!readyToFire) resetShoot();
        if (Input.GetButton("Fire1") && readyToFire)
        {
            weaponAnim.SetTrigger("Shoot");
            weaponAnim.SetBool("Shooting", true);
            if (!shootWithAnim)
            {
                Shoot();
            }
        }
    }


    void resetShoot()
    {
        if (timeToFire <= 0)
        {
            timeToFire = maxTimeToFire;
            readyToFire = true;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    public void Shoot()
    {
        readyToFire = false;
        timeToFire = maxTimeToFire;
        if (projectileWeapon)
        {
            SpawnProjectile();
        }
        else
        {
            ShootRay();
        }
    }

    public void ShootRay()
    {

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, ShootableLayers))
        {
            destination = hit.point;
            hitPoint = hit.transform;
            if (hit.collider.gameObject.GetComponent<PropManager>() != null) hit.collider.GetComponent<PropManager>().getHit(damage, penetration);
            //if(hit.collider.gameObject.GetComponent<Rigidbody>() != null)hit.collider.GetComponent<Rigidbody>().AddExplosionForce(power, destination, 10f);
            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null) hit.collider.GetComponent<Rigidbody>().AddRelativeForce(destination, ForceMode.Impulse);

            //vfx

            hitVFX = weaponAnim.gameObject.GetComponent<WeaponStats>().hitVFX;
            Instantiate(hitVFX, destination, Quaternion.identity);
        }
        else
        {
            hitPoint = null;
        }

    }


    
    public void SpawnProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, ShootableLayers))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000f);
        }
        InstantiateProjectile();
    }
    

    void InstantiateProjectile()
    {
        var projectileObj = Instantiate(projectilePrefab, gunPoint.position, gunPoint.rotation) as GameObject;
        projectileSpeed = projectileObj.GetComponent<Projectile>().projectileSpeed;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - gunPoint.position).normalized * projectileSpeed;
        projectileObj.GetComponent<Projectile>().damage = damage;
        projectileObj.GetComponent<Projectile>().penetration = penetration;
    }


    public void setWeaponStats(float dmg, float pen, float frate, float pwr, bool prct, GameObject prctPrebaf, Transform gnPnt, bool shtAnim)
    {
        damage = dmg;
        penetration = pen;
        timeToFire = frate;
        maxTimeToFire = frate;
        power = pwr;
        projectileWeapon = prct;
        projectilePrefab = prctPrebaf;
        gnPnt = gunPoint;
        shootWithAnim = shtAnim;
    }
}
