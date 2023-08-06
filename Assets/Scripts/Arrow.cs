using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy")) {
            StartCoroutine(DelayedCoroutine.delayedCoroutine(0.1f, () => Destroy(gameObject)));
        }
    }
    
}
