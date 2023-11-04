using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 firePoint;
    [SerializeField]
    private float projectileSpeed;
    void Start()
    {
        firePoint = transform.position;
    }

   
    void Update()
    {
        MoveProjectile();
    }
    void MoveProjectile()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
