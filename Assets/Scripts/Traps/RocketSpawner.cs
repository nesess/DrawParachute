using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform _playerTransform;
    [SerializeField]
    private Lean.Pool.LeanGameObjectPool _spawner;

    [SerializeField]
    private Transform _spawnParent;

    [SerializeField]
    private float _rocketDelay;

    private WaitForSeconds _rocketSpawnSeconds;

    private void Start()
    {
        _rocketSpawnSeconds = new WaitForSeconds(_rocketDelay);
        StartCoroutine(RocketSpawnCoroutine());
    }

    private IEnumerator RocketSpawnCoroutine()
    {
        while(!GameManager.Instance.isPlayerDead)
        {
            yield return _rocketSpawnSeconds;
            GameObject rocket = _spawner.Spawn(Vector3.zero,Quaternion.identity,transform);
            rocket.transform.position = new Vector3(Random.Range(-4.6f, 4.6f), _playerTransform.position.y + 9f, _playerTransform.position.z);
            rocket.GetComponent<Rocket>().SetTarget(_playerTransform.position,_spawner);   
        }
    }

}
