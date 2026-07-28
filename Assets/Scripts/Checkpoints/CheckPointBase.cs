using JetBrains.Annotations;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActivated = false;
    private string checkpointKey = "CheckPointKey";


    private void OnTriggerEnter(Collider other)
    {
        

        if(!checkpointActivated && other.transform.tag == "Player")
        {
            CheckCheckPoint();
        }
    }

    private void CheckCheckPoint()
    {

        TurnItOn();
        SaveCheckPoint();
    }

    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
        SaveCheckPoint();
        
    
    }
    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);


    }
    private void SaveCheckPoint()
    {
        if (PlayerPrefs.GetInt(checkpointKey, 0) > key)
        {
            PlayerPrefs.SetInt(checkpointKey, key);
        }
        checkpointActivated = true;
    }

}
