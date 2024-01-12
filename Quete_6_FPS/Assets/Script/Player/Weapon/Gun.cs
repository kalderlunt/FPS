using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData _gunData;

    public void Shoot()
    {
        Debug.Log("Shot Gun!");
    }
}
