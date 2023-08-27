namespace Cards
{
    public class GainAttackAbility : Ability
    {
        private Stat _gainAttack;
        public GainAttackAbility(Stat gainAttack)
        {
            _gainAttack = gainAttack;
        }
        public override void Apply(Card source, Card target)
        {
            _source = source;
            if (_source.Health<_source._health)
            {
                _source._attack += _gainAttack.Value;
            }
        }

        public override void Cancel()
        {
            _source._attack -= _gainAttack.Value;
        }

        public override void UpdateData(AbilityData data)
        {
            throw new System.NotImplementedException();
        }
    }
}
