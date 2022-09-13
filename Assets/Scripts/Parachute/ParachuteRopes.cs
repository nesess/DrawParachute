using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ParachuteRopes : MonoBehaviour
{
    [SerializeField]
    private Transform _leftHand;
    [SerializeField]
    private Transform _rightHand;

    [SerializeField]
    private Transform _leftRope;
    [SerializeField]
    private Transform _rightRope;
   
    private WaitForEndOfFrame _waitFrame;

   

    public void SetRopes(Transform leftHandConnection, Transform rightHandConnection)
    {

       // MeshCombineManager.Instance.CombineMeshes();
        SetPosAndRot(_leftRope,_leftHand, leftHandConnection);
        SetPosAndRot(_rightRope,_rightHand, rightHandConnection); 
        
        

    }

    public void ResetRopes()
    {
        _leftRope.localScale = Vector3.zero;
        _rightRope.localScale = Vector3.zero;
    }

    private void SetPosAndRot(Transform rope,Transform startPoint,Transform endPoint)
    {
        

        rope.position = (startPoint.position + endPoint.position) / 2;
        rope.rotation = Quaternion.LookRotation(endPoint.position - rope.position);
        float distance = Vector3.Distance(startPoint.position, endPoint.position);
        rope.DOScale(new Vector3(0.05f, 0.05f, distance), 0.3f);

    }

}
