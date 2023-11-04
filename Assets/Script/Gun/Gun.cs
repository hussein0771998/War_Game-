using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform spawn;
    public float shotDistance;
    public void Shoot()
    {
        Ray ray = new Ray(spawn.position, spawn.forward);
        RaycastHit hit;
        shotDistance = 20f;
        if(Physics.Raycast(ray,out hit, shotDistance))
        {
            shotDistance = hit.distance;
        }
        Debug.DrawRay(ray.origin, ray.direction * shotDistance, Color.red, 1);
        Debug.Log("ray.origin = "+ ray.origin+"///"+ " ray.direction = " + ray.direction);
    }
}
