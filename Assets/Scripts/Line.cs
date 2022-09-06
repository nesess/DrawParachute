using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer _renderer;

   

    public void SetPosition(Vector3 pos)
    {
        if(!CanAppend(pos))
        {
            return;
        }

        _renderer.positionCount++;
        _renderer.SetPosition(_renderer.positionCount - 1, pos); //add mouse pos to last point
    }

    private bool CanAppend(Vector3 pos)
    {
        if(_renderer.positionCount == 0) //if first point always return true
        {
            return true;
        }
        
        return Vector3.Distance(_renderer.GetPosition(_renderer.positionCount - 1), pos) > DrawManager.Resolution;
    }

}
