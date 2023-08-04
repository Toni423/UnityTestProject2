using UnityEngine;

public class HouseBehav : Buildable {

    private Builder _builder;
    [SerializeField] private int producedMoney;
    [SerializeField] private float produceTime;
    
    private bool _producing = false;


    private void Start() {
        _builder = GameObject.Find("BuildController").GetComponent<Builder>();
    }

    private void Update() {
        if (!_producing) {
            _producing = true;
            StartCoroutine(DelayedCoroutine.delayedCoroutine(produceTime,() => {
                _builder.addMoney(producedMoney);
                _producing = false;
            }));
        }
    }


}
