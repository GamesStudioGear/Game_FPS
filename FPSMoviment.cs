using UnityEngine;

public class FPSMoviment : MonoBehaviour
{
    public float speed;

    [HideInInspector]
    public float inputX, inputZ;

    private void Start()
    {
        FPSProperties.canMove = true;
    }

    void Update()
    {
        if (FPSProperties.canMove)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            transform.Translate(new Vector3(inputX, 0, inputZ) * speed * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.C))
                FPSProperties.crouched = !FPSProperties.crouched;
        }
    }
}
