namespace Cards
{
    public class SimpleAttackAbility : Ability
    {
        private Stat _attack;
        public SimpleAttackAbility(Stat attack)
        {
            _attack = attack;
        }
        public override void Apply(Card source, Card target)
        {
            _target = target;
            _target.Health -= _attack.Value;
        }

        public override void Cancel()
        {
            _target.Health += _attack.Value;
        }

        public override void UpdateData(AbilityData data)
        {
            throw new System.NotImplementedException();
        }
    }
}