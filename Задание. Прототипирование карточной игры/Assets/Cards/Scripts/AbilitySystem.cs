using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cards
{
    public class AbilitySystem : MonoBehaviour
    {
        [SerializeField]
        private Stat[] _stats;

        public bool TryGetStat(StatType type, out Stat stat)
        {
            stat = _stats.SingleOrDefault(s => s.Type == type);
            return stat.Equals(default(Stat));
        }

        public Stat GetStat(StatType type) => _stats.SingleOrDefault(s => s.Type == type);

        public Dictionary<StatType, Ability> _abilities = new Dictionary<StatType, Ability>();

        private void Awake()
        {
            _abilities.Add(StatType.DealDamage, new DealDamageAbility(GetStat(StatType.DealDamage)));
            _abilities.Add(StatType.BonusDamage, new BonusDamageAbility(GetStat(StatType.BonusDamage)));
            _abilities.Add(StatType.DestroyWeapon, new DestroyWeaponAbility(GetStat(StatType.DestroyWeapon)));
            _abilities.Add(StatType.GainAttack, new GainAttackAbility(GetStat(StatType.GainAttack)));
            _abilities.Add(StatType.GiveMinionBonuses, new GiveMinionBonusesAbility(GetStat(StatType.GiveMinionBonuses)));
            _abilities.Add(StatType.MurlocHaveBonusAttack, new MurlocBonusAttackAbility(GetStat(StatType.MurlocHaveBonusAttack)));
            _abilities.Add(StatType.RestoreHealth, new RestoreAbility(GetStat(StatType.RestoreHealth)));
            _abilities.Add(StatType.SimpleAttack, new SimpleAttackAbility(GetStat(StatType.SimpleAttack)));
        }

        public void ApplyAbilityByType(Card source, Card target, StatType type)
        {
            if (_abilities.TryGetValue(type, out var currentAbility))
            {
                currentAbility.Apply(source, target);
            }
        }

    }
}