using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{	
	private Camera _cam;
	
	[SerializeField, Range(0f, 100f)]
	private float maxDivergenceX;
	[SerializeField, Range(0f, 100f)]
	private float maxDivergenceY;
	
    // Start is called before the first frame update
    void Start()
    {
         _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
		Vector2 obj_pos = transform.position;
		Vector3 cam_pos = _cam.transform.position;
		
		float x_move = Mathf.Max(Mathf.Abs(obj_pos.x-cam_pos.x) - maxDivergenceX, 0f);
		float y_move = Mathf.Max(Mathf.Abs(obj_pos.y-cam_pos.y) - maxDivergenceY, 0f);
		
		_cam.transform.position = new Vector3(Mathf.MoveTowards(cam_pos.x, obj_pos.x, x_move), Mathf.MoveTowards(cam_pos.y, obj_pos.y, y_move), cam_pos.z);
    }
}
