using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerRoutine : Routine
{
    public override void MakeRespawnRoutine() {
        CinemachineVirtualCamera cam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        cam.LookAt = GameObject.Find("Player").transform;
        cam.Follow = GameObject.Find("Player").transform;
    }
}
