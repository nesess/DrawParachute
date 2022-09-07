using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParachuteSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _objectParent;
    [SerializeField]
    private Transform _ParachutePos;

    [SerializeField]
    private Lean.Pool.LeanGameObjectPool _lean;
    [SerializeField]
    private ParachuteRopes _ropes;

    [SerializeField]
    private float tweenTime = 0.3f;
   
    private Vector3[] _rendererPositions;

    [SerializeField]
    private GameObject _halfCirclePrefab;

    [SerializeField]
    private Quaternion _objectRot;

    [SerializeField]
    private Vector3 _positionFixVecotr;
    private Vector3 _objectScale;

    private Transform _leftmostPoint;
    private Transform _rightmostPoint;

    public void GetObjectPos(LineRenderer renderer)
    {

        
        _lean.DespawnAll();
        _objectParent.transform.localPosition = Vector3.zero;    
        
        _rendererPositions = new Vector3[renderer.positionCount];

        renderer.GetPositions(_rendererPositions);
        
        SpawnObjects();

        

    }

    private void SpawnObjects()
    {
        
        for (int i = 0; i < _rendererPositions.Length; i++)
        {
            SpawnAtPos(_rendererPositions[i]);
            
           
            if (i+1 < _rendererPositions.Length) //spawn object if there is too much gap between two
            {
                float distanceTwoPoints;
                distanceTwoPoints = Vector3.Distance(_rendererPositions[i], _rendererPositions[i + 1]);
                Vector3 LastPos = _rendererPositions[i];
                int watcher = 0;
                while (distanceTwoPoints > 0.2f)
                {
                    watcher++;
                    LastPos +=  (_rendererPositions[i + 1] - LastPos) * 0.3f;
                    distanceTwoPoints = Vector3.Distance(LastPos, _rendererPositions[i + 1]);
                    SpawnAtPos(LastPos);
                    
                    
                }
            }
        }
        TweenParachute();
    }

    private Vector3 CalculateMidVector(Vector3 v1,Vector3 v2)
    {
        return v1 + v2 / 2;
    }

    private void SpawnAtPos(Vector3 pos)
    {
        GameObject tempObject;
        Transform tempObjectTransform;
        tempObject = _lean.Spawn(_objectParent, false);
        tempObjectTransform = tempObject.transform;

        tempObjectTransform.localPosition = Vector3.zero;
        tempObjectTransform.localPosition += pos + _positionFixVecotr;
        tempObjectTransform.localRotation = _objectRot;

        
    }

    private void TweenParachute()
    {
        _objectParent.transform.DOMove(_ParachutePos.position, tweenTime).OnComplete(() => _ropes.SetRopes(_leftmostPoint, _rightmostPoint)); 
        for(int i = 0;i<_objectParent.childCount;i++)
        {
            Transform childObject = _objectParent.GetChild(i);

            childObject.transform.DOScale(_objectScale, tweenTime);

            if (i ==0)
            {
                _leftmostPoint = childObject;
                _rightmostPoint = childObject;
            }

            if (_leftmostPoint.localPosition.x > childObject.localPosition.x)
            {
                _leftmostPoint = childObject;
            }
            else if (_rightmostPoint.localPosition.x < childObject.localPosition.x)
            {
                _rightmostPoint =  childObject;
            }

        }
       
    }


    // Start is called before the first frame update
    void Start()
    {
        
        _objectScale = _halfCirclePrefab.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
