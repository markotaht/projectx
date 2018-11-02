using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPos = target.position;
        if(newPos.y < 0)
        {
            newPos.y = 0;
        }
        newPos.z = -10;
        transform.position = newPos;
	}
}
