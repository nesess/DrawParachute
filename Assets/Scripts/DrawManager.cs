using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    private Camera _cam;

    [SerializeField]
    private Line _linePrefab;

    private Line _currentLine;

    [SerializeField]
    private MeshManager _meshManager;

    public const float Resolution = 0.05f;

    void Start()
    {
        _cam = Camera.main;
    }

    
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = _cam.ScreenToWorldPoint(mousePos);
        
       

        if(Input.GetMouseButtonDown(0))
        {
            _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
            
        }

        if(Input.GetMouseButton(0))
        {
            _currentLine.SetPosition(mousePos);
        }

      /*  if(Input.GetMouseButtonUp(0) && _currentLine._renderer.positionCount >2)
        {
            _meshManager.CreateObject(_currentLine._renderer);
        }*/
    }
}
