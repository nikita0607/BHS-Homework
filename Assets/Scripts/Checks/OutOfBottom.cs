using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BHS
{
	public class OutOfBottom : MonoBehaviour
	{
		// Start is called before the first frame update
		
		
		[SerializeField, Range(-100f, 100f)]
		private float _minHeight;

		[SerializeField, Range(0.05f, 1)]
		private float _saveCordsDelta;
		
		private Vector2 _spawnCords;
		private Vector2 _lastCords;
		
		private Rigidbody2D _body;
		private Ground _ground;
        private Respawn _respawn;
		
		private void Awake()
		{
			_body = GetComponent<Rigidbody2D>();
			_ground = GetComponent<Ground>();
            _respawn = GetComponent<Respawn>();
		}

		private void SevaSpawnCords() {
			if (_ground.OnGround && _ground.LastNormal.y >= .9f)
				_spawnCords = _lastCords;
		}


		// Update is called once per frame
		void Update()
		{
			if (_ground.OnGround && _ground.LastNormal.y >= .9f) {
				_lastCords = transform.position;
				Invoke("SevaSpawnCords", _saveCordsDelta);
			}
			
			if (!_ground.OnGround && transform.position.y < _minHeight)
				_respawn.RespawnObject(_spawnCords);
		}
	}
}