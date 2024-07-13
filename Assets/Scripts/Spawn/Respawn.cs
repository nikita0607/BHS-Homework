using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    [SerializeField] private GameObject _prefab;


    public void RespawnObject(Vector2 cords) {
        gameObject.transform.position = cords;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // GameObject clone = Instantiate(_prefab, cords, Quaternion.identity);
        // clone.name = clone.name.Replace("(Clone)", "");
        // Destroy(gameObject);
    }
    public void RespawnObject(float x, float y) {
         RespawnObject(new Vector2(x, y));
    }

    public void RespawnObject(Vector3 cords) {
        RespawnObject(cords.x, cords.y);
    }
}
