using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private Rigidbody2D rb;

    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletForce;



    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            shoot();
        }
    }


    private void FixedUpdate() {
        move();
        rotate();   
    }

    private void rotate() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (mousePos - (Vector2)transform.position).normalized;
        
    }

    private void move() {
        Vector2 movement = Time.fixedDeltaTime * movementSpeed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        //rb.MovePosition(rb.position + movement);
        rb.AddForce(movement, ForceMode2D.Impulse);

    }

    private void shoot() {
        GameObject instBullet = Instantiate(bullet, transform.position + transform.up * 0.1f, transform.rotation);
        instBullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }
}
