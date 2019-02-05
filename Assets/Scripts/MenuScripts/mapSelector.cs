using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelector : MonoBehaviour {

	void OnMouseDown() {
		string name = this.gameObject.name;
		int num = int.Parse(name.Substring (name.Length - 1));

		MapInfo.setMapNum(num);
		SceneManager.LoadScene(name);
	}
}
