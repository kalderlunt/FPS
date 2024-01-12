using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectil;
    public int force = 10;

    [Header("WeaponValues")]
    public Transform gunBarrel;

    [Range(0.000001f, 1.0f)] public float fireRate = 2.0f;
    private float _shotTimer;

    private void Start()
    {

    }

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
                        Vector3 shootDirection = -gunBarrel.forward;
                        bullet.GetComponent<Rigidbody>().velocity = shootDirection * force;

                        _shotTimer = 0.0f;
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
        _shotTimer += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (_shotTimer > fireRate)
            {
                Shoot();
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            _shotTimer = 0.0f;
        }
    }
}