using UnityEngine;

namespace BHS
{
    public class Wall : MonoBehaviour
    {
        [SerializeField, Range(1f, 5f)] public float GravityScale;
        public bool OnWall { get; private set; }
        public float Friction { get; private set; }
		public Vector2 LastNormal { get; private set; }

        private Vector2 _normal;
        private PhysicsMaterial2D _material;

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnWall = false;
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
				
                if ((Mathf.Abs(_normal.x) == 1f)) {
					OnWall = true;	
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
