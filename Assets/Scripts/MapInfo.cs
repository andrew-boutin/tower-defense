using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Contains all information relative to each map
public class MapInfo : MonoBehaviour {
	public const string airCraftCarrierName = "airCraftCarrier", battleShipName = "battleShip", cruiserName = "cruiser",
	destroyerName = "destroyer", galleonName = "galleon", rowBoatName = "rowBoat", 
	sailBoatName = "sailBoat", speedBoatName = "speedBoat", superYachtName = "superYacht";

	private static int mapNum; // Gets set when scene is loaded, used to access all other information

	public const int squarePixelWidth = 48; // Size of grid squares

	public const int numGridSquares = 144;

	public const int gridPixelWidth = 576;

	public const int borderPixelWidth = 12;

	public static void setMapNum(int newMapNum){
		mapNum = newMapNum;
	}

	public static int getMapNum(){
		return mapNum;
	}
}