using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    public Transform target;
    public Transform finish;

    [SerializeField]
    private float YOffset;
    private bool intro = true;
    private float introcounter;
    private float maxIntroCounter = 6f;

	// Use this for initialization
	void Start () {
        //intro = true;
        introcounter = maxIntroCounter;
        Vector3 newPos = finish.position;
        if (newPos.y < 0 + YOffset)
        {
            newPos.y = 0 + YOffset;
        }
        newPos.z = -10;
        transform.position = newPos + Vector3.up * YOffset;
    }
	
	// Update is called once per frame
	void Update () {
        if (intro)
        {
            introcounter -= Time.deltaTime;
            if(introcounter < 0)
            {
                intro = false;
                Camera.main.orthographicSize = 5;
                //transform.GetComponent<CharacterControllerRb>().StartGame();
                return;
            }

            Vector3 newPos = finish.position * introcounter / maxIntroCounter + target.position * (maxIntroCounter - introcounter) / maxIntroCounter;
            Camera.main.orthographicSize = 5 + 10 * Mathf.Sin(Mathf.PI * introcounter / maxIntroCounter);
            if (newPos.y < 0 - YOffset)
            {
                newPos.y = 0 - YOffset;
            }
            newPos.z = -10;
            transform.position = newPos + Vector3.up * YOffset; ;
        }
        else
        {
            Vector3 newPos = target.position;
            if (newPos.y < 0 - YOffset)
            {
                newPos.y = 0 - YOffset;
            }
            newPos.z = -10;
            transform.position = newPos + Vector3.up * YOffset; ;
        }
	}

    public bool IsIntro()
    {
        return intro;
    }
}
