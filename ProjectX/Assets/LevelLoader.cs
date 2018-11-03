using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorToPrefab {
	public Color32 color;
	public GameObject prefab;
}

public class LevelLoader : MonoBehaviour {

	public Texture2D LevelMap;
	
	public ColorToPrefab[] colorToPrefab;
	
	// Use this for initialization
	void Start () {
		LoadMap();
	}
	
	void EmptyMap() {
		//Find all of our children and eliminate them.
		
		while(transform.childCount > 0) {
			Transform c = transform.GetChild(0);
			c.SetParent(null); //no parent
			Destroy(c.gameObject); //destroy no parent
			
	}
}
	void LoadMap() {
		EmptyMap();
		
		//Get the raw pixels from the level imagemap
	Color32[] allPixels = LevelMap.GetPixels32();
	int width = LevelMap.width;
	int height = LevelMap.height;
	
	for(int x = 0; x < width; x++) {
	for(int y = 0; y < height; y++) {
		SpawnTileAt( allPixels[(y * width) + x], x, y );
			}
	
		}

	}
	
	void SpawnTileAt( Color32 c, int x, int y ) {
		
		//If this is a transparent pixel then it´s meant to just be empty.
		if(c.a <= 0) {
			return;
		}
		
		//Find the right color in our map
		
		//NOTE: This isn´t optimized.		
		foreach(ColorToPrefab ctp in colorToPrefab) {
		if( c.Equals(ctp.color) ) {
			
				//Spawn the prefab at the right location
				GameObject go = (GameObject)Instantiate(ctp.prefab, new Vector3(x, y, 0), Quaternion.identity );

                if(go.GetComponent<CharacterControllerRb>() != null)
                {
                    Camera.main.GetComponent<CameraFollow>().target = go.transform;
                }
				//maybe do more stuff to the gameobject here?
				return;
			}
		}
		//If we got to this point, it means we did not find a matching color in our array.
		
		Debug.LogError("No color to prefab found for:" + c.ToString() );
		
		
	}
	
}