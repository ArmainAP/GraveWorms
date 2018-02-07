using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gravestone : MonoBehaviour {
    public bool IsSpecial;
    public bool IsOccupied;

    public float Appetite;
    public float Food = 50.0f;

    public Text FoodText;

	// Use this for initialization
	void Start () {
        if (IsSpecial)
        {
            Food = 100.0f;
            FoodText.text = "Food: " + Mathf.FloorToInt(Food).ToString();
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Rich");
        }
        else
            this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/GraveStone");
    }
	
	// Update is called once per frame
	void Update () {
        if (IsOccupied)
        {
            Food = Food - Time.deltaTime * Appetite;
            FoodText.text = "Food: " + Mathf.FloorToInt(Food).ToString();
        }

        if(Food < 0.0f)
        {
            Destroy(this.gameObject, 0.0f);

            GameObject.FindGameObjectWithTag("Player").GetComponent<WormMovement>().CanMove = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<WormStats>().IsEating = false;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>().Spawn();

            if (IsSpecial)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<WormStats>().AddWormSegment();
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameScript>().Spawn();
            }
        }
	}

    public void Occupy(float appetite)
    {
        Appetite = appetite;
        IsOccupied = true;
    }
}
