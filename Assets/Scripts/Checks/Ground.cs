using UnityEngine;

namespace BHS
{
    public class Ground : MonoBehaviour
    {
        public bool OnGround { get; private set; }
        public float Friction { get; private set; }
		public Vector2 LastNormal { get; private set; }

        private Vector2 _normal;
        private PhysicsMaterial2D _material;

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnGround = false;
            LastNormal = new Vector2(0, 0);
            Friction = 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }

        private void EvaluateCollision(Collision2D collision)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                _normal = collision.GetContact(i).normal;
				
                if (_normal.y >= 0.9f) {
					OnGround = true;	
					LastNormal = _normal;
				}
            }
        }

        private void RetrieveFriction(Collision2D collision)
        {
			Friction = 0;
			
			if (!collision.rigidbody) return;
            _material = collision.rigidbody.sharedMaterial;

            
            if(_material != null)
            {
                Friction = _material.friction;
            }
        }
    }
}
