using Unity.VisualScripting;
using UnityEngine;

public class FPSAnimations : MonoBehaviour
{
    private Animator anim;
    private FPSMoviment fpsMoviment;
    void Start()
    {
        anim = GetComponent<Animator>();
        fpsMoviment = GetComponent<FPSMoviment>();
    }

    void Update()
    {
        anim.SetFloat("Horizontal", fpsMoviment.inputX);
        anim.SetFloat("Vertical", fpsMoviment.inputZ);
        anim.SetBool("crouched", FPSProperties.crouched);

    }
}
