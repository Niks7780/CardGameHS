using UnityEngine;
namespace Cards
{
    public abstract class Ability
    {
        protected Card _source, _target;

        public abstract void Apply(Card source, Card target);

        public abstract void Cancel();

        public abstract void UpdateData(AbilityData data);

    }

}
