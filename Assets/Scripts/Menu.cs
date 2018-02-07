using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.transform.GetChild(0).GetComponent<Text>().text = "Best Lenght: " + PlayerPrefs.GetInt("Lenght").ToString();
	}

    public void NextScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
