using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 15f;
    public Rigidbody2D rb;
    public Animator anim;
    Vector2 movement;
    public GameObject playerBody;

    public Tilemap consumablesTilemap;
    public Tilemap obstaclesTilemap;

    public PlayerHealth playerHealth;
// 	public Tile test;

    // Update is called once per frame
    // Better to handle input here
    void Update()
    {
    	movement.x = Input.GetAxisRaw("Horizontal");
    	movement.y = Input.GetAxisRaw("Vertical");

    	anim.SetFloat("Horizontal", movement.x);
    	anim.SetFloat("Vertical", movement.y);
    	anim.SetFloat("Speed", movement.sqrMagnitude);
    }
    // Better not to put physics stuff into update, put into fixedupdate
    // Better to handle movement
    void FixedUpdate()
    {
    	move();
    }
    void move(){
    	rb.velocity = new Vector2(movement.x,movement.y) * moveSpeed;
//    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

	void OnCollisionEnter2D(Collision2D collision){
/*		// Instantiate a tile at every collision
		Vector3Int tilepos = tilemap.WorldToCell(collision.collider.transform.position);
		tilemap.SetTile(tilepos, test);
*/
        // Check if consumables map exists
        if (consumablesTilemap != null && collision.gameObject.layer == LayerMask.NameToLayer("Consumables"))
        {
        	// Initialises to 0
	        Vector3 hitPosition = Vector3.zero;
        	// Run through every point of contact in collision
            foreach (ContactPoint2D hit in collision.contacts)
            {
            	// Calculate the position of the block that it hit
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                // Removes the tile, by getting the Vector3Int position of it
                consumablesTilemap.SetTile(consumablesTilemap.WorldToCell(hitPosition), null);
            }
        }
        // Removes the lights that are with the consumables and give powerups to player
        if (collision.gameObject.layer == LayerMask.NameToLayer("consumablesLights")){
        	switch (collision.gameObject.name){
        		case "Red": moveSpeed += 10f;break;
        		case "Green": playerHealth.gainHealth(10);break;
        		case "Blue": playerBody.transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);break;
        		case "Yellow": endGame();break;
        	}
        	// Removes the lights
        	Destroy(collision.gameObject);
        }
        // Check if obstacles map exists
        if (obstaclesTilemap != null && collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
        	// Initialises to 0
	        Vector3 hitPosition = Vector3.zero;
        	// Run through every point of contact in collision
            foreach (ContactPoint2D hit in collision.contacts)
            {
            	// Calculate the position of the block that it hit
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                // Removes the tile, by getting the Vector3Int position of it
                obstaclesTilemap.SetTile(obstaclesTilemap.WorldToCell(hitPosition), null);
            }
            playerHealth.takeDamage(10);
        }
	}
	public void endGame(){
		Destroy(gameObject);
	}
}
