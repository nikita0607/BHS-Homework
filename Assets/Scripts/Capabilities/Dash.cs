using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BHS {
    public class Dash : MonoBehaviour
    {
        [SerializeField] private float _dashTimeout;
        [SerializeField] private float _dashStrenght;
        [SerializeField] private float _dashStopAxeleration;
        // Start is called before the first frame update
        private bool _canDash;
        
        public bool InDash;

        private float _lastGravityScale;
        private float _curDashVelocity;

        private Controller _controller;
        private Rigidbody2D _body;

        void Awake() {
            _controller = GetComponent<Controller>();
            _body = GetComponent<Rigidbody2D>();

            _canDash = true;
            InDash = false;
            _curDashVelocity = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (InDash)
                CheckInDash();
                
            if (_controller.Input.RetrieveDashInput() && _canDash) {

                _canDash = false;
                Invoke(nameof(ResetDash), _dashTimeout);
                MakeDash();
            }
        }

        private void CheckInDash() {
            _body.velocity = new Vector2(Mathf.MoveTowards(_body.velocity.x, 0, _dashStopAxeleration*Time.deltaTime), 0);
            _curDashVelocity = Mathf.MoveTowards(_curDashVelocity, 0, _dashStopAxeleration*Time.deltaTime);
            _body.gravityScale = 0;

            Debug.Log($"Dash: {_body.velocity.x}, curDashVelocity: {_curDashVelocity}");

            if (_body.velocity.x == 0 || _curDashVelocity == 0) {
                InDash = false;
                _body.gravityScale = _lastGravityScale;
            }
        }

        private void ResetDash() {
            _canDash = true;
        }

        private void MakeDash() {
            InDash = true;

            float direction = _controller.Input.RetrieveMoveInput();
            
            _body.velocity = new Vector2(Mathf.Sign(direction)*_dashStrenght, 0);
            _curDashVelocity = _dashStrenght;
            
            _lastGravityScale = _body.gravityScale; 
        }
    }
}