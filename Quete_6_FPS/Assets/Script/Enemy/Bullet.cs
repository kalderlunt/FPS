using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;

        if (hitTransform.CompareTag("Player"))
        {
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameObject != null)
        {
            Destroy(gameObject, 10.0f);
        }
    }
}