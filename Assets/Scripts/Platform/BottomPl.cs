using UnityEngine;

namespace BHSCamp
{
    public class BottomPl : MonoBehaviour
    {
        [SerializeField] BoxCollider2D _collider;

        private void Awake() {
            //_collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {  
            SetCollisionIgnore(collision, true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            UpdateCollision(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            UpdateCollision(collision);
        }

        private void SetCollisionIgnore(Collider2D targetCollider, bool status) {
            Physics2D.IgnoreCollision(_collider, targetCollider, status);
        }

        private void UpdateCollision(Collider2D collision) {
            GameObject target = collision.gameObject;
            BoxCollider2D targetCollider = target.GetComponent<BoxCollider2D>();


            if (IsTargetUpper(target)) {
                SetCollisionIgnore(targetCollider, false);
            }
            else {
                SetCollisionIgnore(targetCollider, true);
            }
        }

        private bool IsTargetUpper(GameObject target) {
            BoxCollider2D targetCollider = target.GetComponent<BoxCollider2D>();
            Vector2 offset = targetCollider.GetComponent<BoxCollider2D>().offset;

            float targetY = target.transform.position.y+offset.y-targetCollider.size.y/2;
            float objectY = transform.position.y + _collider.size.y/2;
            return objectY < targetY;
        }
    }
}
