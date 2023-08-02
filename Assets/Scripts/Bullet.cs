using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletLifeTime = 3f;

    private void Awake() {
        StartCoroutine(DelayedCoroutine.delayedCoroutine(bulletLifeTime, () => Destroy(gameObject)));
    }

    
}
