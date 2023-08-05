using System;
using UnityEngine;
using UnityEngine.UI;

public class Buildable : MonoBehaviour
{
    [SerializeField] private int life;
    private int _maxLife;
    [SerializeField] protected GameObject healthBar;
    private Image _healthBarGreen;
    private GameObject _canvas;
    [SerializeField] private ParticleSystem particle;
    
    private bool _damageCooldown = false;
    public int price;
    private void OnCollisionStay2D(Collision2D other) {
        if (!_damageCooldown && other.gameObject.CompareTag("Enemy")) {
            _damageCooldown = true;
            life--;
            _healthBarGreen.fillAmount = (float)life / _maxLife;
 
            if (life <= 0) {
                particle.Play();
                Destroy(gameObject, particle.main.duration);
            }
            
            StartCoroutine(DelayedCoroutine.delayedCoroutine(2f, () => _damageCooldown = false));
        }
    }

    private void Awake() {
        _maxLife = life;
        _canvas = GameObject.Find("Canvas");
        healthBar = Instantiate(healthBar, Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.5f)), Quaternion.identity);
        healthBar.transform.SetParent(_canvas.transform);
        _healthBarGreen = healthBar.transform.GetChild(1).GetComponent<Image>();
    }

    private void OnDestroy() {
        Destroy(healthBar);
    }
}
