using Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AbilityData : ScriptableObject
{
    [SerializeField]
    private Stat[] _stats;
    [SerializeField]
    private string _name;

    public bool TryGetStat(StatType type, out Stat stat)
    {
        stat = _stats.SingleOrDefault(s => s.Type == type);
        return stat.Equals(default(Stat));
    }
}
