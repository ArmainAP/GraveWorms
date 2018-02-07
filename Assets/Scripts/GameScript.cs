using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour {
    GameObject Worm;
    WormStats WormStatsScript;

    private bool CountTime = true;
    private float TimeElapsed = 0.0f;

    public Text WormLenghtText;
    public Text WormHungerText;
    public Text TimeElapsedText;

    public LayerMask mask;
    Vector2 SpawnPosition;

    // Use this for initialization
    void Start () {
        GameObject WormGraveStone = Instantiate(Resources.Load("Prefabs/GraveStone"), this.transform) as GameObject;

        Worm = Instantiate(Resources.Load("Prefabs/WormHead"), this.transform) as GameObject;
        WormStatsScript = Worm.GetComponent<WormStats>();

        Worm.GetComponent<WormMovement>().GraveStone = WormGraveStone;
        WormGraveStone.GetComponent<Gravestone>().Occupy(3);
        WormStatsScript.IsEating = true;

        for (int i = 0; i < 3; i++)
            Spawn();

        UpdateText();
    }
	
	// Update is called once per frame
	void Update () {
        if (CountTime)
        {
            TimeElapsed = TimeElapsed + Time.deltaTime;

            UpdateText();
        }
    }

    void UpdateText() {
        WormLenghtText.text = "Lenght: " + (WormStatsScript.WormSegments.Count + 1).ToString();
        WormHungerText.text = "Hunger: " + Mathf.FloorToInt(WormStatsScript.Hunger).ToString();
        TimeElapsedText.text = "Time: " + Mathf.FloorToInt(TimeElapsed).ToString();
    }

    public void Spawn()
    {
        bool CanSpawnHere = false;

        while (!CanSpawnHere)
        {
            SpawnPosition = new Vector2(Random.Range(-5.5f, 5.5f), Random.Range(-3.0f, 1.5f));
            CanSpawnHere = CheckSpawnOverlap();

            if (CanSpawnHere)
                break;
        }

        GameObject GraveStone = Instantiate(Resources.Load("Prefabs/Gravestone"), SpawnPosition, Quaternion.identity) as GameObject;
        GraveStone.GetComponent<Gravestone>().IsSpecial = Random.value > 0.5f;
    }

    bool CheckSpawnOverlap()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.0f, mask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 CenterPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float LeftExtent = CenterPoint.x - width;
            float RightExtent = CenterPoint.x + width;
            float LowerExtent = CenterPoint.y - height;
            float UpperExtent = CenterPoint.y + height;

            if (SpawnPosition.x >= LeftExtent && SpawnPosition.x <= RightExtent)
                if (SpawnPosition.y >= LowerExtent && SpawnPosition.y <= UpperExtent)
                    return false;
        }

        return true;
    }
}
