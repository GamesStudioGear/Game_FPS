using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FPSGunController : MonoBehaviour
{
    public FPSGun[] inventory;
    public FPSGun currentWeapon;
    private FPSAnimations animations;
    public GameObject cameraContainer;
    public string wallTag, floorTag, enemyTag, waterTag;
    public GameObject wallHole, floorHole;
    public GameObject wallImpact, floorImpact, waterImpact, enemyImpact;

    [HideInInspector]
    public bool reloading;

    void Start()
    {
        animations = GetComponent<FPSAnimations>();

        foreach (FPSGun gun in inventory)
        {
            gun.model.SetActive(false);
        }

        currentWeapon.model.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventory.Length > 0)
            StartCoroutine("ChangeGun", 0);

        if (Input.GetKeyDown(KeyCode.Alpha2) && inventory.Length > 1)
            StartCoroutine("ChangeGun", 1);


        if (!reloading)
        {
            if (Input.GetKeyDown(KeyCode.R))
                Reload();

            if (currentWeapon.gunType == Enums.GunType.Automatic)
                FireAutomatic();
            else
                FireSemiAutomatic();
        }
    }

    void FireAutomatic()
    {
        animations.ToFireAutomatic(Input.GetMouseButton(0));
        //implementar tiro automatico
    }

    void FireSemiAutomatic()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(currentWeapon.ammunition == 0)
            {
                Reload();
                return;
            }

            currentWeapon.ammunition = Mathf.Clamp(currentWeapon.ammunition -= 1, 0, currentWeapon.maximumAmmo);

            animations.ToFire();
            cameraContainer.transform.position = cameraContainer.transform.position - (cameraContainer.transform.forward * currentWeapon.recoilForce);

            currentWeapon.fireSound.Play();


            RaycastHit hit;
            var destiny = Camera.main.transform.position + Camera.main.transform.forward * 1000;

            var collided = Physics.Raycast(currentWeapon.firePoint.position, destiny, out hit, 1000);
            Instantiate(currentWeapon.muzzeEffect, currentWeapon.firePoint);
            if (collided)
            {
                if (hit.transform.tag == enemyTag)
                {
                    Instantiate(enemyImpact, hit.point, enemyImpact.transform.rotation);
                    //implementar dano
                }
                else if (hit.transform.tag == wallTag)
                {
                    Instantiate(wallImpact, hit.point, wallImpact.transform.rotation);
                    Instantiate(wallHole, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else if (hit.transform.tag == floorTag)
                {
                    Instantiate(floorImpact, hit.point, floorImpact.transform.rotation);
                    Instantiate(floorHole, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else if (hit.transform.tag == waterTag)
                {
                    Instantiate(waterImpact, hit.point, waterImpact.transform.rotation);
                }
            }
        }      
    }

    void Reload()
    {
        if (currentWeapon.ammunition == currentWeapon.maximumAmmo)
            return;

        if (currentWeapon.ammunitionToReload == 0)
        {
            currentWeapon.noBulletsSound.Play();
            return;
        }

        reloading = true;

        animations.ToReload();
        currentWeapon.reloadSound.Play();

        var difBullets = currentWeapon.maximumAmmo - currentWeapon.ammunition;
        
        if (difBullets > currentWeapon.ammunitionToReload)
            currentWeapon.ammunition += currentWeapon.ammunitionToReload;
        else
            currentWeapon.ammunition += difBullets;

        currentWeapon.ammunitionToReload = Mathf.Clamp(currentWeapon.ammunitionToReload -= difBullets, 0, currentWeapon.ammunitionToReload);
    }

    IEnumerator ChangeGun(int index)
    {
        currentWeapon.animator.SetTrigger("change");

        yield return new WaitForSeconds(0.3f);
        currentWeapon.model.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        currentWeapon = this.inventory[index];

        currentWeapon.model.SetActive(true);
        currentWeapon.animator.Play("get");
    }

}

