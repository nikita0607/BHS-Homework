using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _startWaypoint;
    [SerializeField] private GameObject _endWaypoint;
    [SerializeField] private GameObject _spikePrefab;
    void Start()
    {
        float xDelta = _spikePrefab.GetComponent<BoxCollider2D>().size.x;
        for (float x=_startWaypoint.transform.position.x; x<=_endWaypoint.transform.position.x; x+=xDelta)
            Instantiate(_spikePrefab, new Vector3(x, _startWaypoint.transform.position.y, 0), Quaternion.identity);
        Destroy(_startWaypoint);
        Destroy(_endWaypoint);
    }

}
