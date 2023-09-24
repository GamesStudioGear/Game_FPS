using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSGunController : MonoBehaviour
{
    public FPSGun[] inventory;
    public FPSGun currentWeapon;
    private FPSAnimations animations;

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

        if (Input.GetKeyDown(KeyCode.R))
            Reload();

        if (currentWeapon.gunType == Enums.GunType.Automatic)
            FireAutomatic();
        else
            FireSemiAutomatic();
    }

    void FireAutomatic()
    {
        animations.ToFireAutomatic(Input.GetMouseButton(0));
        //implementar tiro automatico
    }

    void FireSemiAutomatic()
    {
        if(Input.GetMouseButtonDown(0))
            animations.ToFire();

        //implementar tiro semi automatico
    }

    void Reload()
    {
        //Implementar reload
        animations.ToReload();
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

