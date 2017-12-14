using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		string name = this.gameObject.name;
		int num = int.Parse(name.Substring (name.Length - 1));

		MapInfo.setMapNum(num);
		SceneManager.LoadScene(name);
	}
}
