using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzeEffect : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("DestroyObj");
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
