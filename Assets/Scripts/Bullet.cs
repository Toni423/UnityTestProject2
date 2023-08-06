using UnityEngine;


public class Bullet : MonoBehaviour {
    [SerializeField] private float bulletLifeTime = 3f;
    

    private void Awake() {
        StartCoroutine(DelayedCoroutine.delayedCoroutine(bulletLifeTime, () => Destroy(gameObject)));
        StartCoroutine(DelayedCoroutine.delayedCoroutine(0.3f, () => GetComponent<CircleCollider2D>().enabled = true));
    }


    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(DelayedCoroutine.delayedCoroutine(0.1f, () => Destroy(gameObject)));
        }
    }

    
}
