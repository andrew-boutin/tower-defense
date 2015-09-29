using UnityEngine;
using System.Collections;

public class TowerInfo : MonoBehaviour {
	public GameObject mediumTower, lightTower, heavyTower;

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	public GameObject getTowerGameObject(string towerName){
		if(towerName == "mediumTower")
			return mediumTower;
		else if(towerName == "lightTower")
			return lightTower;
		else if(towerName == "heavyTower")
			return heavyTower;

		return null;
	}

	public int getTowerCost(string towerName){
		if(towerName == "mediumTower")
			return MediumTower.buildCost;
		else if(towerName == "lightTower")
			return LightTower.buildCost;
		else if(towerName == "heavyTower")
			return HeavyTower.buildCost;
		
		return -1;
	}
}
