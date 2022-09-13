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

    

    [SerializeField]
    private ParachuteMovement _parachuteMovement;
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
            if(i != 0)
            {
                if(Vector3.Distance(_rendererPositions[i-1], _rendererPositions[i ])>0.1f)
                {
                    SpawnAtPos(_rendererPositions[i]);
                }
            }
            else
            {
                SpawnAtPos(_rendererPositions[i]);
            }
            
            
           
            if (i+1 < _rendererPositions.Length && _rendererPositions.Length<100) //spawn object if there is too much gap between two
            {
                float distanceTwoPoints;
                distanceTwoPoints = Vector3.Distance(_rendererPositions[i], _rendererPositions[i + 1]);
                Vector3 LastPos = _rendererPositions[i];
                while (distanceTwoPoints > 0.2f)
                {

                    LastPos +=  (_rendererPositions[i + 1] - LastPos) * 0.3f;
                    distanceTwoPoints = Vector3.Distance(LastPos, _rendererPositions[i + 1]);
                    SpawnAtPos(LastPos);   
                }
            }
        } 
        TweenParachute();
    }

   

    private void SpawnAtPos(Vector3 pos)
    {
        if(pos.x >4.3 || pos.x<-4.3)
        {
            return;
        }
        GameObject tempObject;
        Transform tempObjectTransform;
        tempObject = _lean.Spawn(_objectParent, false);
        tempObjectTransform = tempObject.transform;
        tempObjectTransform.localPosition = Vector3.zero;  
        tempObjectTransform.localPosition += pos + _positionFixVecotr;
        tempObjectTransform.localPosition = new Vector3(tempObjectTransform.localPosition.x, tempObjectTransform.localPosition.y, 0);
        tempObjectTransform.localRotation = _objectRot;     
    }

    private void TweenParachute()
    {
        _ropes.ResetRopes();
        _objectParent.transform.DOLocalMoveY(_ParachutePos.localPosition.y, tweenTime).OnComplete(() => _ropes.SetRopes(_leftmostPoint, _rightmostPoint));
        GetObjectBounds();
    }

    private void GetObjectBounds()
    {
        for (int i = 0; i < _objectParent.childCount; i++)
        {
            Transform childObject = _objectParent.GetChild(i);

            childObject.transform.DOScale(_objectScale, tweenTime);

            if (i == 0)
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
                _rightmostPoint = childObject;
            }
        }
        _parachuteMovement.AddParachuteForce(_rendererPositions, _rightmostPoint.localPosition.x - _leftmostPoint.localPosition.x);
    }


    // Start is called before the first frame update
    void Start()
    {
        _objectScale = _halfCirclePrefab.transform.localScale;
    }

   
}
