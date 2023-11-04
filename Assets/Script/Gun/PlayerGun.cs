using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public ParticleSystem fire;
    private AudioSource gunAudioSource;
    public AudioClip gunAudioClip;
    [SerializeField]
    Transform firePoint;

    [SerializeField]
    GameObject projecttilePrefab;

    [SerializeField]
    float fireSpeed;
     Rigidbody bulet2, bulet3;
    public static PlayerGun ins;
   // private float lastTimeShoot = 0;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        gunAudioSource = GetComponent<AudioSource>();
    }
    public void Shoot()
    {
        fire.Play();
        if (!gunAudioSource.isPlaying)
        {
            gunAudioSource.PlayOneShot(gunAudioClip);
        }
        GameObject bul1 = Instantiate(projecttilePrefab, firePoint.position, firePoint.rotation);
        GameObject bul2 = Instantiate(projecttilePrefab, firePoint.position, firePoint.rotation);
        GameObject bul3 = Instantiate(projecttilePrefab, firePoint.position, firePoint.rotation);
      
        bulet2 = bul2.GetComponent<Rigidbody>();
        bulet2.velocity = transform.right*5f;
        bulet3 = bul3.GetComponent<Rigidbody>();
        bulet3.velocity = -transform.right*5f;

        Destroy(bul1, 2f); Destroy(bul2, 2f); Destroy(bul3, 2f);

    }
}
