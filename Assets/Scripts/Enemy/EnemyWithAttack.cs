using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace BHSCamp
{
    // враг, который патрулирует и атакует игрока, если тот в зоне видимости
    public class EnemyWithAttack : PatrolEnemy
    {
        [Header("Attack")]
        [SerializeField] protected LayerMask _playerLayerMask;
        [SerializeField] protected Vector2 _attackRange;
        protected AttackBase _attack;
        public bool CanAttack { get; private set; }

        private void Awake()
        {
            _attack = GetComponent<AttackBase>();
            _body = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
        }

        private void Start()
        {
            InitializeStates();
            _fsm.SetState<PatrolState>();
            SetForwardVector(new Vector2(transform.localScale.x, 0));
            CanAttack = true;
        }

        protected override void InitializeStates()
        {
            // кроме состояний родительского класса(PatrolEnemy) добавляем состояние атаки
            base.InitializeStates();
            _fsm.AddState(new AttackState(_fsm, this, _attack));
        }

        private void Update()
        {
            _fsm.Update(Time.deltaTime);
        }

        public virtual bool PlayerInSight()
        {
            RaycastHit2D hit = CheckPlayerHit();
            if (!hit) return false;
            Debug.Log(CheckCanSeePlayer(hit.collider.gameObject));
            return hit.collider.GetComponent<IDamageable>() != null && CheckCanSeePlayer(hit.collider.gameObject);
        }

        public virtual bool CheckCanSeePlayer(GameObject target) {
            GetComponent<Collider2D>().enabled = false;

            Vector2 hitStart = transform.position;
            Vector3 targetColliderOffset = target.GetComponent<Collider2D>().offset;
            Vector2 hitDirection = (targetColliderOffset+target.transform.position-transform.position).normalized;
            float hitDistance = Mathf.Sqrt(Mathf.Pow(_attackRange.x, 2) + Mathf.Pow(_attackRange.y, 2));
            RaycastHit2D hit = Physics2D.Raycast(hitStart, hitDirection, hitDistance, _playerLayerMask);

            Debug.DrawLine(hitStart, hitStart+hitDirection*hitDistance);

            GetComponent<Collider2D>().enabled = true;

            Debug.Log(hit.collider.gameObject);

            if (!hit.collider.gameObject) return false;
            return hit.collider.gameObject == target;
        }

        public virtual RaycastHit2D CheckPlayerHit()
        {
            Vector2 origin = new(
                transform.position.x + (_forwardVector.x * _attackRange.x / 2),
                transform.position.y
            );
            RaycastHit2D hit = Physics2D.BoxCast(
                origin,
                _attackRange,
                0f,
                _forwardVector,
                0,
                _playerLayerMask
            );

            return hit;
        }

        public IEnumerator HandleAttackCD()
        {
            CanAttack = false;
            yield return new WaitForSeconds(_attack.GetAttackCD());
            CanAttack = true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (_forwardVector == null) return;
            Vector2 origin = new(
                transform.position.x + (_forwardVector.x * _attackRange.x / 2),
                transform.position.y
            );
            Gizmos.DrawWireCube(origin, _attackRange);
        }
    }
}