using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BHS {
    public class Teleport : MonoBehaviour
    {
        public Teleport _connectedPortal;
        private GameObject _player;
        private Controller _playerController;
        private bool _teleportEnabled;
        private bool _keyKlicked;


        void Awake()
        {
            _player = GameObject.Find("Player");
            _playerController = _player.GetComponent<Controller>();
        }

        void Update()
        {
            if (_playerController.Input.RetrieveUseInput() && IsOnDistance()) {
                Invoke(nameof(TeleportPlayer), .1f);
            }
        }

        private bool IsOnDistance()
        {
            if (Vector2.Distance(_player.transform.position, transform.position) < 1f)
            {
                return true;
            }
            return false;
        }

        private void TeleportPlayer()
        {
            _player.transform.position = _connectedPortal.transform.position;
            Debug.Log("Teleport");
        }
    }
}