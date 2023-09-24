using UnityEngine;

public class FPSAnimations : MonoBehaviour
{
    private Animator anim;
    private FPSMoviment fpsMoviment;
    private FPSGunController gunController;
    void Start()
    {
        anim = GetComponent<Animator>();
        fpsMoviment = GetComponent<FPSMoviment>();
        gunController = GetComponent<FPSGunController>();
    }

    void Update()
    {
        anim.SetFloat("Horizontal", fpsMoviment.inputX);
        anim.SetFloat("Vertical", fpsMoviment.inputZ);
        anim.SetBool("crouched", FPSProperties.crouched);

        if(gunController.currentWeapon != null)
            gunController.currentWeapon.animator.SetBool("walk", fpsMoviment.inputX != 0 || fpsMoviment.inputZ != 0);
    }

    public void ToReload()
    {
        gunController.currentWeapon.animator.SetTrigger("reload");
    }

    public void ToFireAutomatic(bool pressing)
    {
        gunController.currentWeapon.animator.SetBool("fire", pressing);
    }

    public void ToFire()
    {
        gunController.currentWeapon.animator.SetTrigger("fire");
    }
}
