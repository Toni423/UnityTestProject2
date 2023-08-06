using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Plyeranimation : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private Sprite down;
    [SerializeField] private Sprite left;
    [SerializeField] private Sprite right;
    [SerializeField] private Sprite up;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        _mainCamera = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        rotate();   
    }

  
    private void rotate() {
        if (_mainCamera == null)
        {
            return;
        }
        Vector2 direction = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Vector2.SignedAngle(transform.right + transform.up , direction);
        if (angle < 0) {
            spriteRenderer.sprite = angle < -90f ? down : right;
        }
        else {
            spriteRenderer.sprite = angle > 90f ? left : up;
        }
    }
}
