using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectil;
    public int force = 10;

    [Header("WeaponValues")]
    public Transform gunBarrel;

    private void InstantiateRb(GameObject bullet)
    {
        if (bullet.GetComponent<MeshCollider>().convex == true)
        {
            if (bullet.GetComponent<MeshCollider>().isTrigger == false)
            {
                if (bullet.GetComponent<Rigidbody>().interpolation == RigidbodyInterpolation.Interpolate)
                {
                    if (bullet.GetComponent<Rigidbody>().collisionDetectionMode == CollisionDetectionMode.Continuous)
                    {
                        // kawaboumga BOOOM
                        bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * force);

                    }
                    else
                    {
                        bullet.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                        InstantiateRb(bullet);
                    }
                }
                else
                {
                    bullet.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
                    InstantiateRb(bullet);
                }
            }
            else
            {
                bullet.GetComponent<MeshCollider>().isTrigger = false;
                InstantiateRb(bullet);
            }
        }
        else
        {
            bullet.GetComponent<MeshCollider>().convex = true;
            InstantiateRb(bullet);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectil, gunBarrel.position, Quaternion.identity) as GameObject;
        
        bullet.transform.parent = GameObject.Find("BulletsParent").transform;

        if (bullet.GetComponent<Rigidbody>())
        {
            InstantiateRb(bullet);
        }
        else
        {
            bullet.AddComponent<Rigidbody>();
            InstantiateRb(bullet);
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }
    }
}