using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    private Lean.Pool.LeanGameObjectPool _rocketSpawner;

    [SerializeField]
    private GameObject _explosionFX;
    [SerializeField]
    private float _rocketSpeed;
    private Rigidbody _rigidbody;
    private Vector3 _target;
    private MeshRenderer _meshRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameManager.Instance.PlayerDead();

        }
       
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _meshRenderer.enabled = false;
        _explosionFX.SetActive(true);
        _rocketSpawner.Despawn(gameObject,1.5f);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetTarget(Vector3 target,Lean.Pool.LeanGameObjectPool spawner)
    {
        _rocketSpawner = spawner;
        _target = target;
        Quaternion targetRotation = Quaternion.LookRotation(_target - transform.position);
        transform.rotation = targetRotation;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x + 90, transform.eulerAngles.y, transform.eulerAngles.z);
        _rigidbody.velocity = transform.up * _rocketSpeed;
    }
}
