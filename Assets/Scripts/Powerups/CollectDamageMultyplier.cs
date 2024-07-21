using UnityEngine;

namespace BHSCamp
{
    public class CollectDamagePowerup : TemporaryPowerup
    {
        [SerializeField] private int _damageMultiplier;
        private Health _health;
        private bool _hited;

        public override void Apply(GameObject target)
        {
            _health = target.GetComponent<Health>();
            if (_health == null)
                return;
            
            _health.OnDamageTaken += DamageMultiplier;
            _hited = false;
        }

        public void DamageMultiplier(int damage) {
            if (_hited) {
                _hited = false;
                return;
            }
            _hited = true;
            _health.TakeDamage(damage*_damageMultiplier-damage);
        }

        protected override void OnExpire()
        {
            _health.OnDamageTaken -= DamageMultiplier;
        }
    }
}