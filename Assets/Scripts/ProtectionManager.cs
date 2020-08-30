using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionManager : MonoBehaviour
{
    [SerializeField] GameObject protectionSparkVFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject VFX = Instantiate(protectionSparkVFX,other.transform.position,Quaternion.identity);
        Destroy(VFX, 0.15f);

        other.gameObject.SetActive(false);

    }

}
