using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Transform firePoint;
    public Transform[] patrolPoints;

    private int _currentPointIndex;
    private bool once;
    
    public GameObject bulletPrefab;

    public float speed;
    public float waitTime;
    public float bulletForce = 20f;
    public float timeBetweenShots = 1f;
    private float _nextShootTime;

    [SerializeField] private AudioSource _enemyShootSound;

    // Update is called once per frame
    void Update()
    {
        //Расчёт времени между выстрелами
        if (Time.time > _nextShootTime)
        {
            Shoot();
            _nextShootTime = Time.time + timeBetweenShots;
        }

        //Отвечает за перемещение ИИ между точками для патрулирования
        if (transform.position != patrolPoints[_currentPointIndex].position)
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[_currentPointIndex].position, speed * Time.deltaTime);
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }

        //Сколько времени ИИ простоит на точке
        IEnumerator Wait()
        {
            yield return new WaitForSeconds(waitTime);
            if (_currentPointIndex + 1 < patrolPoints.Length)
                _currentPointIndex++;
            else
                _currentPointIndex = 0;

            once = false;
        }


    }

    private void FixedUpdate()
    {
        //Для направления взгляда на игрока. Пишем в FixedUpdate для плавности, для фиксированного количества кадров
        Vector2 targetDir = target.position - transform.position;
        transform.right = Vector2.Lerp(transform.right, targetDir, speed * Time.deltaTime);
    }
    void Shoot()
    {
        //Функция выстрела ИИ
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        _enemyShootSound.Play();
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.collider.tag == "PlayerBullet")
        {
            FindObjectOfType<GameManager>().PlayerWin();
            //Debug.Log("Колизия сработала");
        }
    }

   }
