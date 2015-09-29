using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Contains all information relative to each map
public class MapInfo : MonoBehaviour {
	public static int mapNum; // Gets set when scene is loaded, used to access all other information

	public static int gridPixelWidth = 32; // Size of grid squares
}