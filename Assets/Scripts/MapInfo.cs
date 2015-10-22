using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Contains all information relative to each map
public class MapInfo : MonoBehaviour {
	private static int mapNum; // Gets set when scene is loaded, used to access all other information

	private static int squarePixelWidth = 48; // Size of grid squares

	private static int numGridSquares = 144;

	private static int gridPixelWidth = 576;

	private static int borderPixelWidth = 12;

	public static void setMapNum(int newMapNum){
		mapNum = newMapNum;
	}

	public static int getMapNum(){
		return mapNum;
	}

	public static int getSquarePixelWidth(){
		return squarePixelWidth;
	}

	public static int getNumGridSquares(){
		return numGridSquares;
	}

	public static int getGridPixelWidth(){
		return gridPixelWidth;
	}

	public static int getBorderPixelWidth(){
		return borderPixelWidth;
	}
}