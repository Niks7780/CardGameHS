namespace Cards
{
    public class MurlocBonusAttackAbility : Ability
    {
        private Stat _murlocAttack;
        public MurlocBonusAttackAbility(Stat murlocAttack)
        {
            _murlocAttack = murlocAttack;
        }
        public override void Apply(Card source, Card target)
        {
            _target = target;
            if (_target._typeText.Equals(CardUnitType.Murloc))
            {
                _target._attack += _murlocAttack.Value;
            }
        }

        public override void Cancel()
        {
            _target._attack -= _murlocAttack.Value;
        }

        public override void UpdateData(AbilityData data)
        {
            throw new System.NotImplementedException();
        }
    }
}