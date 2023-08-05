using UnityEngine;

public class HouseBehav : Buildable {

    private Builder _builder;
    [SerializeField] private int producedMoney;
    [SerializeField] private float produceTime;

    private Animator _animator;
    private bool _producing = false;


    private void Start() {
        _builder = GameObject.Find("BuildController").GetComponent<Builder>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!_producing) {
            _producing = true;
            StartCoroutine(DelayedCoroutine.delayedCoroutine(produceTime,() => {
                _animator.SetTrigger("Producing");
                _builder.addMoney(producedMoney);
                _producing = false;
            }));
        }
    }

    

}
