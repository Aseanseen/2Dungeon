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

    public Tilemap consumablesTilemap;
    public Tilemap obstaclesTilemap;

    public PlayerHealth playerHealth;
    public Transform hitCircle;
    bool isDead = false;

    float attackOffsetLR = 3f;
    float attackOffsetUD = 4.3f;
// 	public Tile test;

    void Start(){
    	hitCircle.position = transform.position + new Vector3(0,-attackOffsetUD,0);
    }
    // Update is called once per frame
    // Better to handle input here
    void Update()
    {
    	movement.x = Input.GetAxisRaw("Horizontal");
    	movement.y = Input.GetAxisRaw("Vertical");

    	anim.SetFloat("Horizontal", movement.x);
    	anim.SetFloat("Vertical", movement.y);
    	anim.SetFloat("Speed", movement.sqrMagnitude);

    	// Remembers the last move and sets the animation
    	if ((movement.x == 1) || (movement.x == -1) || (movement.y == 1) || (movement.y == -1)){
    		anim.SetFloat("lastMoveX", movement.x);
    		anim.SetFloat("lastMoveY", movement.y);
    		// changes the position of the hitCircle
    		if (movement.x == 1){
    			hitCircle.position = transform.position + new Vector3(attackOffsetLR,0,0);
    		}
    		if (movement.x == -1){
    			hitCircle.position = transform.position + new Vector3(-attackOffsetLR,0,0);
    		}
    		if (movement.y == 1){
    			hitCircle.position = transform.position + new Vector3(0,attackOffsetUD,0);
    		}
			if (movement.y == -1){
    			hitCircle.position = transform.position + new Vector3(0,-attackOffsetUD,0);
    		}
    	}
    }
    // Better not to put physics stuff into update, put into fixedupdate
    // Better to handle movement
    void FixedUpdate()
    {
    	if (!isDead){
    		move();
    	}
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
		if (!isDead){
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
	        		case "Red": speedUp();break;
	        		case "Green": heal();break;
	        		case "Blue": scaleDown();break;
	        		case "Yellow": endGame();break;
	        	}
	        	// Removes the lights
	        	Destroy(collision.gameObject);
	        }

	        // Check if obstacles map exists
	        if (obstaclesTilemap != null && collision.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
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
	            if (collision.gameObject.name == "FallingChest"){
	            	endGame();
	            }
	            else{
	            	// Damage player
	            	hurt();
	            }
	        }
    	}
	}
	public void endGame(){
		die();
	}
	public void speedUp(){
		moveSpeed += 10f;
	}
	public void scaleDown(){
		transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
	}
	void die(){
		isDead = true;
		anim.SetBool("isDead",true);
		playerHealth.setHealth(0);
		moveSpeed = 0f;
		rb.gravityScale = 9.81f;
	}
	void heal(){
		playerHealth.gainHealth(30);
	}
	void hurt(){
        // Damage player
        playerHealth.takeDamage(10);
        anim.SetTrigger("Hurt");
	}
}
