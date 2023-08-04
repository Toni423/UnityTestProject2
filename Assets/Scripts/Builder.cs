using UnityEngine;

public class Builder : MonoBehaviour
{
    private GameObject buildable;
    private Camera _mainCamera;
    
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && buildable != null)
        {
            build();
        }
    }

    private void build()
    {
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(buildable, mousePos, Quaternion.identity);
    }

    public void setBuildable(GameObject buildable) {
        this.buildable = buildable;
    }
}
