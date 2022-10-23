using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float timeBetweenShots = 1f;
    public float bulletForce = 20f;
    private float _nextShootTime;

    [SerializeField] private AudioSource _playerShootSound;

    void Update()
    {
        //������� ��� �������� ����� ����������
        if (Input.GetButtonDown("Fire1") && Time.time > _nextShootTime)
        {
            Shoot();
            _nextShootTime = Time.time + timeBetweenShots;
        }
    }

    void Shoot()
    {
        //������� ��������
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        CinemaMachineShake.Instance.ShakeCamera(7f, .1f);
        _playerShootSound.Play();
    }


}
