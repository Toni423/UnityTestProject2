using UnityEngine;

public class HouseBehav : Buildable {

    private CanvasController _canvas;
    [SerializeField] private int producedScore;
    [SerializeField] private float produceTime;
    
    private bool _producing = false;


    private void Start() {
        _canvas = GameObject.Find("Canvas").GetComponent<CanvasController>();
    }

    private void Update() {
        if (!_producing) {
            _producing = true;
            StartCoroutine(DelayedCoroutine.delayedCoroutine(produceTime,() => {
                _canvas.addScore(producedScore);
                _producing = false;
            }));
        }
    }


}
