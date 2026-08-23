using UnityEngine;

[CreateAssetMenu (fileName = "NewSOInt", menuName = "So/Int")]

public class SOInt : ScriptableObject
{
    public int valueInt;

    public void Add(int amount)
    {
        valueInt += amount;
    }

}
