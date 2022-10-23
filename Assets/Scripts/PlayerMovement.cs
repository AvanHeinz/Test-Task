using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    public static int playerScore { get; private set; }
    bool gameHasEnded = false;
    public float restartDelay = 1f;

    [SerializeField] private AudioSource _footstepSound;


    void Update()
    {
        //����������� ������
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.magnitude > 0.5f && !_footstepSound.isPlaying)
        {
            //��� ��� ������ ������
            _footstepSound.volume = Random.Range(0.4f, 0.7f);
            _footstepSound.pitch = Random.Range(0.5f, 0.7f);
            _footstepSound.Play();
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //��� ��� �������� ������ ����� ��� � ������� ����
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        //����� �������� ���� � ���� ����� ��������� ������ � �����
        if (collisionInfo.collider.tag == "EnemyBullet")
        {
            FindObjectOfType<GameManager>().EnemyWin();
            //Debug.Log("������� ���������");
        }

    }
    }
