using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private GameObject enemy;
    [SerializeField] private float ovalMajorRadius = 21f; // Major radius
    [SerializeField] public float ovalMinorRadius = 10f; // Minor radius
    private Vector2 _center = Vector2.zero ;

    [SerializeField] private float rechargeTime;
    private bool recharging;


    private void Update() {
        if (!recharging) {
            recharging = true;
            spawn();
            StartCoroutine(DelayedCoroutine.delayedCoroutine(rechargeTime, () => recharging = false));
        }
    }


    private void spawn() {
        Instantiate(enemy, getSpawnPosition(), Quaternion.identity);
    }

    private Vector2 getSpawnPosition() {
        // Generate a random angle in radians (0 to 2π)
        float theta = Random.Range(0f, 2f * Mathf.PI);

        // Calculate (x, y) coordinates on the oval
        float x = _center.x + ovalMajorRadius * Mathf.Cos(theta);
        float y = _center.y + ovalMinorRadius * Mathf.Sin(theta);

        return new Vector2(x, y);
    }
    
}
