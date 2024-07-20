using UnityEngine;

namespace BHSCamp
{
    public class CollectDamagePowerup : TemporaryPowerup
    {
        [SerializeField] private int _damageMultiplier;
        private Health _health;

        public override void Apply(GameObject target)
        {
            _health = target.GetComponent<Health>();
            if (_health == null)
                return;
            
            _health.OnDamageTaken += DamageMultiplier;
        }

        public void DamageMultiplier(int damage) {
            _health.TakeDamage(damage*_damageMultiplier-damage);
        }

        protected override void OnExpire()
        {
            _health.OnDamageTaken -= DamageMultiplier;
        }
    }
}