namespace Cards
{
    public class DestroyWeaponAbility : Ability
    {
        private Stat _destroyWeapon;
        public DestroyWeaponAbility(Stat destroyWeapon)
        {
            _destroyWeapon = destroyWeapon;

        }
        public override void Apply(Card source, Card target)
        {
            _target = target;
            _destroyWeapon.Value = _target._attack;
            _target._attack -= _destroyWeapon.Value;
        }

        public override void Cancel()
        {
            _target._attack += _destroyWeapon.Value;
        }

        public override void UpdateData(AbilityData data)
        {
            throw new System.NotImplementedException();
        }
    }
}