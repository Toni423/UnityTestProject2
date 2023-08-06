using UnityEngine;

public class Archer : Buildable
{
    [SerializeField] private LayerMask inView;
    private ContactFilter2D _viewContactFilter = new ContactFilter2D();
    private CircleCollider2D _fieldOfView;
    [SerializeField] private float reloadTime;
    private bool _reloading;
    [SerializeField] private GameObject arrow;
    [SerializeField] private float shootStrength;
    
    private void Start() {
        _viewContactFilter.SetLayerMask(inView);
        _fieldOfView = GetComponent<CircleCollider2D>();
    }

    private void Update() {
        if (!_reloading) {
            _reloading = true;
            Collider2D nearest = detectNearestObject();
            if (nearest == null) {
                _reloading = false;
                return;
            }
            Vector2 target = (nearest.transform.position - transform.position).normalized;
            
            GameObject instArrow = Instantiate(arrow, transform.position, Quaternion.identity);
            instArrow.transform.right = -target;
            instArrow.GetComponent<Rigidbody2D>().AddForce(target * shootStrength, ForceMode2D.Impulse);

            StartCoroutine(DelayedCoroutine.delayedCoroutine(reloadTime, () => _reloading = false));

        }
    }


    private Collider2D detectNearestObject() {
        Collider2D[] inView = new Collider2D[1];
        _fieldOfView.OverlapCollider(_viewContactFilter, inView);
        Vector2 currentPos = transform.position;

        Vector2 shortestVector = (Vector2) Vector2.positiveInfinity;
        Collider2D result = null;
        

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
}
