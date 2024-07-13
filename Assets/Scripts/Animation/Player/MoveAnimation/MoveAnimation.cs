using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BHS
{
	public class MoveAnimation : MonoBehaviour
	{	
		[SerializeField] private GameObject _spriteObject;

		private bool _isJumping;
		private float _xDirection;
		
		private Ground _ground;
		private Wall _wall;
		private Animator _anim;
		private Rigidbody2D _body;
		private Controller _controller;
		
		private void Awake() {
			_anim = GetComponentInChildren<Animator>();
			_body = GetComponent<Rigidbody2D>();
			_ground = GetComponent<Ground>();
			_wall = GetComponent<Wall>();
			_controller = GetComponent<Controller>();
		}
		
		void Update()
		{

			_isJumping = _ground.OnGround | _wall.OnWall;
			_xDirection = _controller.Input.RetrieveMoveInput();

			Vector3 scale = transform.localScale;
			scale.x = Mathf.Abs(scale.x);

			if (_ground.OnGround && _xDirection != 0) {
				scale.x = _xDirection > 0 ? scale.x : -scale.x;
			}

			if (_wall.OnWall && _wall.LastNormal.x != 0) {
				scale.x = _wall.LastNormal.x > 0 ? scale.x : -scale.x;
			}

			if (_xDirection != 0 && !_wall.OnWall || _wall.OnWall && _wall.LastNormal.x != 0)
				transform.localScale = scale;
			
			_anim.SetBool("IsJumping", !_isJumping);
			_anim.SetBool("OnWall", _wall.OnWall);
			_anim.SetBool("OnGround", _ground.OnGround);
			_anim.SetFloat("XVelocity", Mathf.Abs(_body.velocity.x));
			_anim.SetFloat("YVelocity", _body.velocity.y);
		}
	}
}