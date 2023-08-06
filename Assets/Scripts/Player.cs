using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    private Rigidbody2D _rb;
    private Camera _mainCamera;
 

    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private float bulletForce;

    [Header("PlayerData")]
    [SerializeField] private int life = 3;
    

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            shoot();
        }
    }
    

    private void move() {
        Vector2 movement = Time.fixedDeltaTime * movementSpeed * new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        _rb.AddForce( movement, ForceMode2D.Impulse);

    }

    private void shoot() {
        GameObject instBullet = Instantiate(bullet, transform.position, transform.rotation);
        instBullet.GetComponent<Rigidbody2D>().AddForce((_mainCamera.ScreenToWorldPoint(Input.mousePosition)- transform.position).normalized  * bulletForce, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            life--;
        }

        if (life <= 0) {
            Destroy(gameObject);
        }
    }
}
