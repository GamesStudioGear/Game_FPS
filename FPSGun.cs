using UnityEngine;
using static Enums;

public class FPSGun : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    public GunType gunType;
    public string gunName;
    public float recoilForce;
    public int ammunition;
    public int maximumAmmo;
    public int ammunitionToReload;
    public Transform firePoint;
    public GameObject muzzeEffect;
    public AudioSource fireSound;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public GameObject model;


    void Awake()
    {
        animator = GetComponent<Animator>();
        model = this.gameObject;
    }

}
