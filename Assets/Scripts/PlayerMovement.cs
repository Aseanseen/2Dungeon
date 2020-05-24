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
    
    public Tilemap obstaclesTilemap;

    public PlayerHealth playerHealth;
    public Transform hitCircle;
    bool isDead = false;
    bool isSlippery = false;

    float attackOffsetLR = 3f;
    float attackOffsetUD = 4.3f;
// 	public Tile test;

    int damage = 10;

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
    		move(isSlippery);
    	}
    }
    void move(bool isSlippery){
		// Allows for knockback of the player
    	if (isSlippery){
    		rb.AddForce(movement * moveSpeed, ForceMode2D.Force);
    	}
    	else{
    		rb.velocity = movement * moveSpeed;
    	}
//    	rb.velocity = new Vector2(movement.x,movement.y) * moveSpeed;
//    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

	void OnCollisionEnter2D(Collision2D collision){
/*		// Instantiate a tile at every collision
		Vector3Int tilepos = tilemap.WorldToCell(collision.collider.transform.position);
		tilemap.SetTile(tilepos, test);
*/
		if (!isDead){
	        if (collision.gameObject.layer == LayerMask.NameToLayer("Consumables")){
	        	Destroy(collision.gameObject);
	        	switch (collision.gameObject.tag){
	        		case "RedPowerUp": slippery();break;
	        		case "GreenPowerUp": heal();break;
	        		case "BluePowerUp": scaleDown();break;
	        		case "End": endGame();break;
	        	}
	        }

	        // Check if obstacles map exists
	        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles")){
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
	            	hurt(damage);
	            }
	        }
    	}
	}
	public void endGame(){
		die();
	}
	public void slippery(){
		isSlippery = true;
	}
	public void scaleDown(){
		transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
	}
	void die(){
		isDead = true;
		anim.SetBool("isDead",true);
		playerHealth.setHealth(0);
		moveSpeed = 0f;
		rb.gravityScale = 8f;
        // Stop enemy from hitting player
        gameObject.layer = LayerMask.NameToLayer("Enemy");
	}
	void heal(){
		playerHealth.gainHealth(30);
	}
	public void hurt(int damage){
        // Damage player
        playerHealth.takeDamage(damage);
        anim.SetTrigger("Hurt");
	}
}
