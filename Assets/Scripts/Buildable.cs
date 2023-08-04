using UnityEngine;

public class Buildable : MonoBehaviour
{
    [SerializeField] private int life;
    
    private bool _damageCooldown = false;
    public int price;
    private void OnCollisionStay2D(Collision2D other) {
        if (!_damageCooldown && other.gameObject.CompareTag("Enemy")) {
            _damageCooldown = true;
            life--;

            if (life <= 0) {
                Destroy(gameObject);
            }
            
            StartCoroutine(DelayedCoroutine.delayedCoroutine(2f, () => _damageCooldown = false));
        }
    }
}
