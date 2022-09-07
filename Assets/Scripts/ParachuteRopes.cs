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


    // Start is called before the first frame update
    void Start()
    {
        _waitFrame = new WaitForEndOfFrame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   


    public void SetRopes(Transform leftHandConnection, Transform rightHandConnection)
    {
        
        
        SetPosAndRot(_leftRope,_leftHand, leftHandConnection);
        SetPosAndRot(_rightRope,_rightHand, rightHandConnection); 
        
        

    }

    private void SetPosAndRot(Transform rope,Transform startPoint,Transform endPoint)
    {
        Debug.Log("Start pos: " + startPoint.position + "  end pos: " + endPoint.position);
        //rope.localScale = Vector3.zero;
        rope.position = (startPoint.position + endPoint.position) / 2;
        rope.localRotation = Quaternion.LookRotation(endPoint.position);
        float distance = Vector3.Distance(startPoint.position, endPoint.position);
       // rope.DOScale(new Vector3(0.2f, 0.2f, distance), 0.5f);

    }

}
