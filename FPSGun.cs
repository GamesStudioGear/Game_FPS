using TMPro;
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
    public AudioSource noBulletsSound;
    public AudioSource reloadSound;
    public TextMeshProUGUI txtAmmunition;
    public TextMeshProUGUI txtammunitionToReload;

    [HideInInspector]
    public Animator animator;

    [HideInInspector]
    public GameObject model;


    void Awake()
    {
        animator = GetComponent<Animator>();
        model = this.gameObject;
    }

    private void Update()
    {
        txtAmmunition.text = ammunition.ToString("00");
        txtammunitionToReload.text = ammunitionToReload.ToString("00");
    }

    public void StopReload()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FPSGunController>().reloading = false;
    }
}
