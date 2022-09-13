using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParachuteMovement : MonoBehaviour
{
    private Rigidbody _rigid;

    [SerializeField]
    private Transform _parachutePos;

    [SerializeField]
    private float parachuteForce = 5;

    [SerializeField]
    private float _maxVelocity;

    private float _averageX = 0;
    private Vector3 _rotVector;
    private float _maxRot = 30;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        _rigid.centerOfMass = Vector3.zero;
        _rigid.inertiaTensorRotation = Quaternion.identity;
        _rotVector = Vector3.zero;
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, _maxVelocity);
        _rotVector.z = Mathf.Clamp(transform.rotation.z,-_maxRot, _maxRot);
        transform.eulerAngles = _rotVector;

    }

    public void AddParachuteForce(Vector3[] list,float parachuteWidth)
    {
       
        for(int i = 0;i<list.Length;i++)
        {
            _averageX += list[i].x;
        }
        _averageX = _averageX / list.Length;
        float yForce;
        switch (parachuteWidth)
        {
            case <1:
                yForce = -2;
                break;
            case <2:
                yForce = -1;
                break;
            case <3:
                yForce = 0;
                break;
            default:
                yForce = 1;
                break;
        }
        Vector3 force = new Vector3(_averageX * parachuteForce, yForce, 0);
        _rigid.AddForceAtPosition(force,_parachutePos.position, ForceMode.VelocityChange);
        _rigid.AddForce(force, ForceMode.VelocityChange);
    }

}
