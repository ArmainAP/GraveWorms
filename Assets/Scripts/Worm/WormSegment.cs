using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSegment : MonoBehaviour {

    public int Order;
    private Transform Head;

    private Vector3 SegmentVelocity;

	// Use this for initialization
	void Start () {
        Head = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(Order == 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Head.position, ref SegmentVelocity, 0.1f);
            transform.up = Head.transform.up;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, Head.GetComponent<WormStats>().WormSegments[Order - 1].position, ref SegmentVelocity, 0.1f);
            transform.up = Head.transform.up;
        }

        this.GetComponent<PolygonCollider2D>().enabled = Head.GetComponent<PolygonCollider2D>().enabled;
	}
}
