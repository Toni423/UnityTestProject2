using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Builder : MonoBehaviour
{
    private Camera _mainCamera;

    [Header("Buildables")] 
    [SerializeField] private GameObject house;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject knight;

    [Header("Sprites")] 
    [SerializeField] private Sprite houseSprite;
    [SerializeField] private Sprite wallSprite;
    [SerializeField] private Sprite knightSprite;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI moneyText;
    private int _money = 500;
    private SpriteRenderer _currentSprite;
    private buildableEnum _selected = buildableEnum.House;

    [Header("General")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private CinemachineShake cinemachineShake;
    [SerializeField] private float buildShakeTime;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _currentSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            build();
        }
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        transform.position = mousePos;

        if (_selected == buildableEnum.Wall) {
            transform.Rotate(Vector3.forward, Input.mouseScrollDelta.y * rotationSpeed * Time.deltaTime);
        }
        
        moneyText.SetText("" + _money);
    }

    private void build()
    {
        //Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        GameObject toBuild;
        switch (_selected) {
            case buildableEnum.House : toBuild = house;
                break;
            case buildableEnum.Wall : toBuild = wall;
                break;
            case buildableEnum.Knight : toBuild = knight;
                break;
            default: return;
        }

        if (toBuild.GetComponent<Buildable>().price <= _money) {
            _money -= toBuild.GetComponent<Buildable>().price;
            Instantiate(toBuild, transform.position, transform.rotation);
            cinemachineShake.shake(0.5f, buildShakeTime);
        }
    }

    public void selectHouse() {
        _selected = buildableEnum.House;
        _currentSprite.sprite = houseSprite;
        transform.up = Vector3.up;
    }
    public void selectWall() {
        _selected = buildableEnum.Wall;
        _currentSprite.sprite = wallSprite;
    }
    public void selectKnight() {
        _selected = buildableEnum.Knight;
        _currentSprite.sprite = knightSprite;
        transform.up = Vector3.up;
    }

    private object _locker = new();
    public void addMoney(int amount) {
        lock (_locker) {
            _money += amount;
        }
    }
}
