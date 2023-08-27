namespace Cards
{
    public class BonusDamageAbility : Ability
    {
        private Stat _bonusDamage;
        public BonusDamageAbility(Stat bonusDamage)
        {
            _bonusDamage = bonusDamage;

        }
        public override void Apply(Card source, Card target)
        {
            _source = source;
            _source._attack += _bonusDamage.Value;

        }

        public override void Cancel()
        {
            _source._attack -= _bonusDamage.Value;
        }

        public override void UpdateData(AbilityData data)
        {
            throw new System.NotImplementedException();
        }
    }
}