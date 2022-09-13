using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DrawManager : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IPointerExitHandler 
{
    private bool _drawing;
    private bool _spawning;

    private Camera _cam;

    [SerializeField]
    private Line _linePrefab;

    [SerializeField]
    private ParachuteSpawner _parachuteSpawner;

    private Line _currentLine;

    [SerializeField]
    private MeshManager _meshManager;

    public const float Resolution = 0.1f;



  

    public void OnPointerDown(PointerEventData eventData)
    {
        _drawing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(_currentLine != null)
        {
            StopDrawing();
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {

         if(_currentLine != null)
        {
            StopDrawing();
        }
    }

    private void StopDrawing()
    {
        _drawing = false;
        if (!_spawning && _currentLine._renderer.positionCount > 1)
        {
            _spawning = true;
            Destroy(_currentLine.gameObject);
            _parachuteSpawner.GetObjectPos(_currentLine._renderer);
        }
    }
    

    void Start()
    {
        _cam = Camera.main;
 
    }

    
    
    void Update()
    {

        if(_drawing)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -_cam.transform.position.z;
            
            mousePos = _cam.ScreenToWorldPoint(mousePos);
          



            if (Input.GetMouseButtonDown(0))
            {
                _spawning = false;
                Vector3 spawnPos = Vector3.zero;
                spawnPos.z = -_cam.transform.position.z;
                _currentLine = Instantiate(_linePrefab,spawnPos, Quaternion.identity);
                _currentLine.transform.SetParent(_cam.transform, false);

            }

            if (Input.GetMouseButton(0))
            {
                Vector3 drawPos = mousePos;
                drawPos.y -= _cam.transform.position.y;
                _currentLine.SetPosition(drawPos);
                if(_currentLine._renderer.positionCount>99)
                {
                    StopDrawing();
                }
            }
           

            /*  if(Input.GetMouseButtonUp(0) && _currentLine._renderer.positionCount >2)
              {
                  _meshManager.CreateObject(_currentLine._renderer);
              }*/
        }

    }

   
}
