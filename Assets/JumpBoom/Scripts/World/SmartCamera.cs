using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmartCamera : MonoBehaviour
{
    public Camera cam;

    public Transform[] followedPoints;
    public float border = 5;

    public float smoothSpeed = 4;

    public string followDynamic = "Player";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        var followed = followedPoints.Concat(GameObject.FindGameObjectsWithTag(followDynamic).Select(gobj => gobj.transform));
        if (followed.Count() == 0)
        {
            return;
        }

	    float minY = followed.OrderBy(point => point.position.y).First().position.y - border;
	    float maxY = followed.OrderByDescending(point => point.position.y).First().position.y + border;
	    float minX = followed.OrderBy(point => point.position.x).First().position.x - border;
	    float maxX = followed.OrderByDescending(point => point.position.x).First().position.x + border;

	    Vector3 target = new Vector3(0,0,0);
        target.x = (minX + maxX) / 2.0f;
	    target.y = (minY + maxY) / 2.0f;
	    target.z = Mathf.Min(-CalculateDepth(Mathf.Abs(maxY - minY)), -CalculateDepth(Mathf.Abs(maxX - minX)));
	    this.transform.position = Vector3.Lerp(this.transform.position, target, Time.deltaTime * smoothSpeed);
	}

    private float CalculateDepth(float expectedHeight)
    {
        var distance = (expectedHeight * 0.5f) / Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        return distance;
    }
}
