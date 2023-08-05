using System;
using UnityEngine;


public class Enemy : MonoBehaviour {
    
    [SerializeField] private int life = 3;
    [SerializeField] private float movementSpeed;

    private GameObject _player;
    private PolygonCollider2D _fieldOfView;

    [SerializeField] private LayerMask inView;
    private ContactFilter2D _viewContactFilter = new ContactFilter2D();
    
    private bool _damageCooldown = false;
    
    
    private void Awake() {
        _viewContactFilter.SetLayerMask(inView);
        _player = GameObject.FindGameObjectWithTag("Player");
        _fieldOfView = GetComponent<PolygonCollider2D>();
    }

    
    
    private void FixedUpdate() {
        move();
    }
    
    
    private void move() {
        Vector2 target = detectNearestObject().transform.position;
        Vector2 currentPos = transform.position;

        transform.up = (target - currentPos).normalized;

        // _rb.AddForce(Time.fixedDeltaTime * movementSpeed * ((detectNearestObject()).normalized), ForceMode2D.Force);
        transform.position = Vector2.MoveTowards(currentPos, target, movementSpeed * Time.fixedDeltaTime);
    }

    private Collider2D detectNearestObject() {
        Collider2D[] inView = new Collider2D[1];
        _fieldOfView.OverlapCollider(_viewContactFilter, inView);
        Vector2 currentPos = transform.position;

        Vector2 shortestVector = (Vector2) _player.transform.position - currentPos;
        Collider2D result = _player.GetComponent<Collider2D>();

        if (inView.Length == 0) {
            return result;
        }
        
        foreach (Collider2D curr in inView) {
            if (curr == null) {
                break;
            }
            Vector2 currWay = (Vector2) curr.transform.position - currentPos;

            if (currWay.magnitude < shortestVector.magnitude) {
                shortestVector = currWay;
                result = curr;
            }
        }

        return result;
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bullet")) {
            life--;
        }
        if (life <= 0) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (!_damageCooldown && other.gameObject.CompareTag("Knight")) {
            _damageCooldown = true;
            life--;
            StartCoroutine(DelayedCoroutine.delayedCoroutine(2f, () => _damageCooldown = false));
        }
        if (life <= 0) {
            Destroy(gameObject);
        }
    }
}
