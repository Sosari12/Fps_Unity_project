using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTypes : MonoBehaviour
{
    public int currentWeapon;
    private Shooting shootingScript;
    public GameObject[] weaponsModels;

    private Animator currentAnimator;

    // Start is called before the first frame update
    void Start()
    {
        shootingScript = gameObject.GetComponent<Shooting>();
        currentWeapon = 0;
        WeaponStats statHolder = weaponsModels[0].GetComponent<WeaponStats>();
        shootingScript.setWeaponStats(statHolder.damage, statHolder.penetration, statHolder.weaponsFireRate, statHolder.power, statHolder.projectileWeapon, statHolder.projectile, statHolder.gunPoint, statHolder.shootWithAnim);
        currentAnimator = weaponsModels[0].GetComponent<Animator>();
        shootingScript.weaponAnim = currentAnimator;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != 0)
        {
            shootingScript.projectileWeapon = true;
            switchWeapon();
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != 1)
        {
            switchWeapon();
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentWeapon != 2)
        {
            switchWeapon();
            currentWeapon = 2;
        }

    }

    public void switchWeapon()
    {
        currentAnimator.SetTrigger("Hide");
        //StartCoroutine(SetStats(currentWeapon));
    }

    public void AfterHide()
    {
        StartCoroutine(SetStats(currentWeapon));
    }


    IEnumerator SetStats(int weaponNumber)
    {
        for(int i = 0;  i < weaponsModels.Length; i++)
        {

            if(i != weaponNumber)
            {
                weaponsModels[i].SetActive(false); 
            }
            else
            {
                weaponsModels[i].SetActive(true);
                if(weaponsModels[i].GetComponent<Animator>() != null)currentAnimator = weaponsModels[i].GetComponent<Animator>();
                WeaponStats statHolder = weaponsModels[i].GetComponent<WeaponStats>();
                shootingScript.setWeaponStats(statHolder.damage, statHolder.penetration, statHolder.weaponsFireRate, statHolder.power, statHolder.projectileWeapon, statHolder.projectile, statHolder.gunPoint, statHolder.shootWithAnim);
                shootingScript.weaponAnim = currentAnimator;
            }
        }

        yield return new WaitForSeconds(0.05f);
    }
}

//changed weapon type chages shooting damage pen and shoot speed