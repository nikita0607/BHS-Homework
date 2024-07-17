using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(Collider2D))]
    public class PeriodicDamageDealer : MonoBehaviour
    {
        [Header("MODE: Periodic damage")]
        [SerializeField] private int _periodicDamage;
        [SerializeField] private float _periodicDamageCooldown;
        private float _periodicDamageTimer;

        private void Awake() {
            _periodicDamageTimer = 0f;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _periodicDamageTimer += Time.deltaTime;
            if (_periodicDamageTimer >= _periodicDamageCooldown) {
                DealPeriodicDamage(collision.gameObject.GetComponent<IDamageable>());
                _periodicDamageTimer = 0f;
            }
        }

        // STEP 9: Сейчас урон наносится каждый раз, когда вызывается OnTriggerStay2D,
        // т.е. каждый кадр обработки физики
        // сделайте так, чтобы урон наносился каждые _periodicDamageCooldown секунд (по таймеру)

        private void DealPeriodicDamage(IDamageable damageable)
        {
            if (damageable == null) return;

            damageable.TakeDamage(_periodicDamage);
            MonoBehaviour mb = (MonoBehaviour) damageable;
            print($"Dealt {_periodicDamage} to {mb.name}");
        }
    }
}