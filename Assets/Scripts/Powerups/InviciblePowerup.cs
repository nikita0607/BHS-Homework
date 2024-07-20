using UnityEngine;

namespace BHSCamp
{
    public class InviciblePowerup : TemporaryPowerup
    {
        private GameObject _player;

        public override void Apply(GameObject target)
        {
            _player = target;
            int _layer = LayerMask.NameToLayer("PlayerInvicible");
            _player.layer = _layer;
            base.Apply(target);
        }

        protected override void OnExpire()
        {
            int _layer = LayerMask.NameToLayer("Player");
            _player.layer = _layer; // восстанавливаем слой игрока
        }
    }
}