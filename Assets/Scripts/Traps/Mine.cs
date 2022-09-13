using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    private GameObject _explosionFX;

    private void OnTriggerEnter(Collider other)
    {
        Vector3 midPoint = (other.gameObject.transform.position + transform.position)/2;
        _explosionFX.transform.position = midPoint;
        _explosionFX.SetActive(true);
        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        GameManager.Instance.PlayerDead();
    }
}
