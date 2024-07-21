using System.Numerics;
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
            UpdateCollision(collision);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            UpdateCollision(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            UpdateCollision(collision);
        }

        private void UpdateCollision(Collider2D collision) {
            GameObject target = collision.gameObject;
            BoxCollider2D targetCollider = target.GetComponent<BoxCollider2D>();


            if (IsTargetUpper(target)) {
                Physics2D.IgnoreCollision(_collider, targetCollider, false);
            }
            else {
                Physics2D.IgnoreCollision(_collider, targetCollider, true);
            }
        }

        private bool IsTargetUpper(GameObject target) {
            BoxCollider2D targetCollider = target.GetComponent<BoxCollider2D>();

            float targetY = target.transform.position.y-targetCollider.size.y/2;
            float objectY = transform.position.y + _collider.size.y/2;
            return objectY < targetY;
        }
    }
}
