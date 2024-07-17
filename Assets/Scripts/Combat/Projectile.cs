using UnityEngine;

namespace BHSCamp
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _body;

        void Awake() {
            _body = GetComponent<Rigidbody2D>();
        }


        public void OnCollisionEnter2D(Collision2D collision) {
            Destroy(gameObject);
        }

        public void SetDirection(Vector2 direction)
        {
            _body.velocity = direction * _speed;  // устанавливаем направление движения проджектайла
        }
    }
}