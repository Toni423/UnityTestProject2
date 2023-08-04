using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0;

    void Update()
    {
        _scoreText.SetText("" + _score);
    }


    private Object _locker = new();
    public void addScore(int add)
    {
        lock (_locker)
        {
            _score += add;
        }
    }
}
