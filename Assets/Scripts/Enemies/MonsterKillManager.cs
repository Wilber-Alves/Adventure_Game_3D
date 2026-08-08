using EDGEE.Core.Singleton;
using UnityEngine;

public class MonsterKillManager : Singleton<MonsterKillManager>
{
    public SOInt totalMonsterKills;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        if (totalMonsterKills != null)
            totalMonsterKills.valueInt = 0;
    }

    public void AddKill()
    {
        if (totalMonsterKills != null)
        {
            totalMonsterKills.valueInt++;
            Debug.Log("Monster Defeated! Total: " + totalMonsterKills.valueInt);
        }
    }
}