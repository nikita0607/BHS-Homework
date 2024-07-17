using BHSCamp.FSM;

namespace BHSCamp
{
    public class IdleState : FsmState
    {
        private PatrolEnemy _enemy;
        private float _exitTime;
        private float _timer;

        public IdleState(Fsm fsm, PatrolEnemy enemy, float exitTime) : base(fsm)
        {
            _enemy = enemy;
            _exitTime = exitTime;
        }

        public override void Enter()
        {
            _timer = 0; 
        }

        // STEP 4: Сейчас из данного состояния можно перейти только в PatrolState
        // сделайте так, чтобы, при обнаружении игрока, осуществлялся переход в AttackState
        public override void Update(float deltaTime)
        {
            if (_timer > _exitTime)
                Fsm.SetState<PatrolState>();
            CheckForPlayer();
        }

        private void CheckForPlayer()
        {
            
            // STEP 3: Реализуйте логику:
            // Если противник является атакующим И игрок в его зоне видимости:
            //     Переходим в состояние атаки
            //
            if (_enemy is EnemyWithAttack && ((EnemyWithAttack)_enemy).PlayerInSight())
                Fsm.SetState<AttackState>();
            // 
            // STEP 4: Реализуйте такую же логику в состоянии IdleState
        }
    }
}