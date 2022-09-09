using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCombineManager : MonoBehaviour
{
    private MeshCombiner _meshCombiner;

    

    public static MeshCombineManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _meshCombiner = GetComponent<MeshCombiner>();
       
    }

    public void CombineMeshes()
    {
        
        _meshCombiner.CombineMeshes(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
