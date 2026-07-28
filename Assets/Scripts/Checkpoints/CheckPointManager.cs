using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using EDGEE.Core.Singleton;

public class CheckPointManager : Singleton <CheckPointManager>
{
    public int lastCheckPointKey = 0;

    public List<CheckPointBase> checkpoints;

    public bool HasCheckPoint()
    { 
        return lastCheckPointKey > 0;
    
    }
    public void SaveCheckPoint(int i)
    {
        if (lastCheckPointKey < i)
        { 
            lastCheckPointKey = i;
        }
        
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    
    }

}
