using UnityEngine;

namespace BHS
{
    [RequireComponent(typeof(Controller))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float _jumpHeight = 3f;
        [SerializeField, Range(0, 5)] private int _maxAirJumps = 0;
        [SerializeField, Range(0f, 5f)] private float _downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float _upwardMovementMultiplier = 1.7f;
		[SerializeField, Range(0f, 100f)] private float _xJumpSpeed;
        [SerializeField, Range(0f, 5f)] private float _jumpDesiredTime;

        private Controller _controller;
        private Rigidbody2D _body;
        private Ground _ground;
        private Wall _wall;
        private Vector2 _velocity;
        private Dash _dash;

        private int _jumpPhase;
        private float _defaultGravityScale, _jumpSpeed;
		
		private float _xJumpSpeedMul;
		private bool _frameSkip;

        private bool _desiredJump, _onGround, _onWall;

        void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
            _wall = GetComponent<Wall>();
            _controller = GetComponent<Controller>();
            _dash = GetComponent<Dash>();

            _defaultGravityScale = 1f;
			_xJumpSpeedMul = 0f;
			_frameSkip = false;
        }

        void Update()
        {
            _desiredJump |= _controller.Input.RetrieveJumpInput();
            if (_desiredJump )
                Invoke("ResetJump", _jumpDesiredTime);
        }

        private void ResetJump() {
            _desiredJump = false;
        }

        private void FixedUpdate()
        {
            _onGround = _ground.OnGround;
            _onWall = _wall.OnWall;
            _velocity = _body.velocity;
			
			if (_frameSkip) {
				_frameSkip = false; // ХЗ почему но без фрэймскипа он думает что стоит на стене даже когда отпрыгивает от нее 	
				return;
			}

            if (_onGround || _onWall)
            {
                _jumpPhase = 0;
            }

            if (_desiredJump)
            {
                JumpAction();
                
            }

            if (!_dash.InDash) {
                if (_body.velocity.y > 0)
                {
                    _body.gravityScale = _upwardMovementMultiplier;
                }
                else if (_body.velocity.y < 0)
                {
                    _body.gravityScale = _downwardMovementMultiplier;

                    if (!_ground.OnGround && _wall.OnWall)
                        _body.gravityScale /= _wall.GravityScale;
                }
                else if(_body.velocity.y == 0)
                {
                    _body.gravityScale = _defaultGravityScale;
                }
            }

            _body.velocity = _velocity;
        }
        private void JumpAction()
        {
            if (_onGround || _onWall || _jumpPhase < _maxAirJumps)
            {
                _jumpPhase += 1;
				ResetJump();
				_xJumpSpeedMul = _jumpPhase == 1 ? _wall.LastNormal.x*0.5f : 0;
				if (_xJumpSpeed > 0f) _frameSkip = true;
                
                _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _jumpHeight);
                
                if (_velocity.y > 0f)
                {
                    _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
                }
                else if (_velocity.y < 0f)
                {
                    _jumpSpeed += Mathf.Abs(_body.velocity.y);
                }
				
                _velocity.y += _jumpSpeed;
				if (Mathf.Abs(_xJumpSpeedMul)*_xJumpSpeed > 0f )
					_velocity.x = _xJumpSpeed*_xJumpSpeedMul;
            }
        }
    }
}

