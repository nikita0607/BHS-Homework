using System.Collections;
using BHSCamp.FSM;
using UnityEngine;

namespace BHSCamp
{
    public class DeadState : FsmState
    {
        private PatrolEnemy _enemy;
        private IHealable _health;
        private Animator _animator;
        private bool _respawn;
        private float _respawnTime;
        private float _respawnTimer;
        private int _respawnHealAmount;

        public DeadState(Fsm fsm, PatrolEnemy enemy, IHealable health, bool respawn, float respawnTime, int respawnHealAmount) : base(fsm)
        {
            _respawn = respawn;
            _respawnTime = respawnTime;
            _respawnTimer = 0;
            _animator = enemy.GetComponent<Animator>();
            _enemy = enemy;
            _health = health;
            _respawnHealAmount = respawnHealAmount;
        }

        public override void Enter()
        {
            _respawnTimer = 0;
            _animator.SetBool("IsDead", true);
        }

        public override void Exit() {
            _animator.SetBool("IsDead", false);
        }

        public override void Update(float deltaTime) {
            if (!_respawn) return;

            _respawnTimer += deltaTime;

            if (_respawnTimer >= _respawnTime)
                Respawn();
        }

        private void Respawn() 
        {
            Fsm.SetState<PatrolState>();
            _health.TakeHeal((int)_respawnHealAmount);
        }
        // STEP 11: Если _respawn == true,
        // через _respawnTime секунд мы должны выйти из состояния Dead
        // и восстановить здоровье
    }
}