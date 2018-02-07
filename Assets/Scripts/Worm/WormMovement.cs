using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormMovement : MonoBehaviour {
    public float Speed = 3.0f;
    public bool CanMove = false;

    public GameObject GraveStone;

    float TimeFromRelease;
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 MouseScreenPostion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.up = (MouseScreenPostion - (Vector2)transform.position).normalized;

            if (!CanMove)
            {
                CanMove = true;

                Destroy(GraveStone.gameObject, 0.0f);
                if (GraveStone.GetComponent<Gravestone>().IsSpecial)
                {
                    this.GetComponent<WormStats>().AddWormSegment();
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>().Spawn();
                }

                TimeFromRelease = 0.0f;
            }
        }

        if (CanMove)
        {
            TimeFromRelease += Time.deltaTime;
            MoveForward();

            if (TimeFromRelease > 0.5f)
            {
                this.GetComponent<PolygonCollider2D>().enabled = true;
                GraveStone = null;
            }
        }
        else
            transform.position = GraveStone.transform.position;
	}

    void MoveForward() {
        transform.position += transform.up * Time.deltaTime * Speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Border")
            transform.up = Vector3.Reflect(transform.up, collision.contacts[0].normal);

        if(collision.transform.tag == "Gravestone" && collision.gameObject != GraveStone)
        {
            this.GetComponent<PolygonCollider2D>().enabled = false;
            GraveStone = collision.gameObject;
            transform.position = GraveStone.transform.position;
            CanMove = false;

            GraveStone.GetComponent<Gravestone>().Occupy(this.GetComponent<WormStats>().WormSegments.Count + 1);
            this.GetComponent<WormStats>().IsEating = true;
        }
    }
}
