using UnityEngine;
using System.Collections;

public class TowerInfo : MonoBehaviour {
	public GameObject mediumTower, lightTower, heavyTower;

	// These values don't change
	private static int lightTowerBuildCost = 200, mediumTowerBuildCost = 250, heavyTowerBuildCost = 300;

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

	public static int getTowerCost(string towerName){
		if(towerName == "mediumTower")
			return mediumTowerBuildCost;
		else if(towerName == "lightTower")
			return lightTowerBuildCost;
		else if(towerName == "heavyTower")
			return heavyTowerBuildCost;
		
		return -1;
	}
}
