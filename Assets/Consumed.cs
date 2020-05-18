using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Consumed : MonoBehaviour
{
	public Tilemap tilemap;
//	public Tile test;
	void OnCollisionEnter2D(Collision2D collision){
/*		// Instantiate a tile at every collision
		Vector3Int tilepos = tilemap.WorldToCell(collision.collider.transform.position);
		tilemap.SetTile(tilepos, test);
*/
		// Initialises to 0
        Vector3 hitPosition = Vector3.zero;

        // Check if map exists
        if (tilemap != null)
        {
        	// Run through every point of contact in collision
            foreach (ContactPoint2D hit in collision.contacts)
            {
            	// Calculate the position of the block that it hit
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                // Removes the tile, by getting the Vector3Int position of it
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
	}
}
