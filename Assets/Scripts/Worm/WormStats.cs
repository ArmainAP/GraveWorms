using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WormStats : MonoBehaviour {
    public List<Transform> WormSegments;

    public float Hunger;
    public bool IsEating = false;

    // Use this for initialization
    void Start () {
        AddWormSegment();
        AddWormSegment();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!IsEating)
                Hunger -= WormSegments.Count + 1;

            IsEating = false;
        }

        UpdateHunger();
        }

    void UpdateHunger() {
        if (IsEating)
            Hunger = Hunger + Time.deltaTime;
        else
            Hunger = Hunger - Time.deltaTime * (WormSegments.Count + 1) / 2;

        if (Hunger > 100.0f)
            Hunger = 100.0f;

        if(Hunger < 0.0f)
        {
            if (PlayerPrefs.GetInt("Lenght") < WormSegments.Count + 1)
                PlayerPrefs.SetInt("Lenght", WormSegments.Count + 1);

            SceneManager.LoadScene("Menu");
        }
    }

    public void AddWormSegment() {
        if(WormSegments.Count == 0)
        {
            Vector3 CurrentPosition = transform.position;
            GameObject NewWormSegment = Instantiate(Resources.Load("Prefabs/WormBody"), CurrentPosition, Quaternion.identity) as GameObject;
            NewWormSegment.GetComponent<WormSegment>().Order = WormSegments.Count;
            WormSegments.Add(NewWormSegment.transform);
        }
        else
        {
            Vector3 CurrentPosition = WormSegments[WormSegments.Count - 1].position;
            GameObject NewWormSegment = Instantiate(Resources.Load("Prefabs/WormBody"), CurrentPosition, Quaternion.identity) as GameObject;
            NewWormSegment.GetComponent<WormSegment>().Order = WormSegments.Count;
            WormSegments.Add(NewWormSegment.transform);
        }
    }
}
