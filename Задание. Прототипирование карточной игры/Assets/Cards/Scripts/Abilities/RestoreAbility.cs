namespace Cards
{
    public class RestoreAbility : Ability
    {
        private Stat _restore;
        public RestoreAbility(Stat restore)
        {
            _restore = restore;
        }
        public override void Apply(Card source, Card target)
        {
            _source = source;
            _source.Health += _restore.Value;
        }

        public override void Cancel()
        {
            _source.Health -= _restore.Value;
        }

        public override void UpdateData(AbilityData data)
        {
            throw new System.NotImplementedException();
        }
    }
}
