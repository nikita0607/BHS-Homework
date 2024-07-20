using UnityEngine;

namespace BHSCamp
{
    public class PoisonPowerup : TemporaryPowerup
    {

        [SerializeField] private int _damage;
        [SerializeField] private int _kickCount;
        private GameObject _player;

        public override void Apply(GameObject target)
        {
            _player = target;

            IDamageable hp = target.GetComponent<IDamageable>();
            hp.TakeDamage(_damage);
            base.Apply(target);
        }   

        protected override void OnExpire()
        {
            _kickCount--;
            if (_kickCount <= 0)
                return;
            Debug.Log("ADSD");
            Apply(_player);
        }
    }
}