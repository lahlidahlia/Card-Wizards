using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;
    private Vector2 targetPosition;
    [System.NonSerialized]
    public bool isChanneling = false;

    private Deck deck;
	private Hand hand;

	public Text handText;
	public Text deckText;
	public Text cooldownText;

	public Timer cooldownTimer;
	// Use this for initialization
	void Start () {
		cooldownTimer = new Timer();
        deck = new Deck(new List<int>() { 1,1,1,2,2,2,3,3,3,3 });
		deck.Shuffle();
		hand = new Hand(deck);
		while (hand.Draw()) { }  // Draw until hand is full.
	}
	
	// Update is called once per frame
	void Update () {
        // Get mouse coordinate.
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (Input.GetMouseButton(0)) {  // Left click.
            Debug.Log(deck.ToString());
        }
        if (Input.GetMouseButton(1)) {  // Right click to move.
            targetPosition = new Vector2(mouseX, mouseY);
        }

        // Each key correspond to each hand slot.
        int inputKey = -1;  // -1 is no press.
		int heldKey = -1;  // Used for channeling purposes.
		// The reason getkeydown is nested within getkey is so that key press and key hold is differentiated.
        if (Input.GetKey("1")) {
			heldKey = 1;
			if (Input.GetKeyDown("1")) {
				inputKey = 1;
			}
        } else if (Input.GetKey("2")) {
			heldKey = 2;
			if (Input.GetKeyDown("2")) {
				inputKey = 2;
			}
		} else if (Input.GetKey("3")) {
			heldKey = 3;
			if (Input.GetKeyDown("3")) {
				inputKey = 3;
			}
		} else if (Input.GetKey("4")) {
			heldKey = 4;
			if (Input.GetKeyDown("4")) {
				inputKey = 4;
			}
		}
        if (inputKey != -1) {  // When a spell key is being pressed.
			// Cast a spell if not channeling and not on cooldown.
            if (!isChanneling && cooldownTimer.GetState()) {
				Spell spellUsed = hand.Use(inputKey - 1, gameObject);
				cooldownTimer.Set(spellUsed.cooldown);  // Set player's cooldown.
				hand.Draw();
            }
        } 
		if(heldKey == -1){  // When no key is held.
            if (isChanneling == true) {  // When the player release key while channeling:
				Debug.Log("UNCHANNELING!");
                isChanneling = false;
            }
        }

        if (!isChanneling) {
            lookAtMouse();
            moveToward(targetPosition, speed);
        }

		cooldownTimer.Tick();

		// Draw hand.
		handText.text = hand.ToString();
		deckText.text = deck.ToString();
		cooldownText.text = cooldownTimer.GetTime().ToString();
	}
    
    void lookAtMouse(){
        // Get mouse coordinates.
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        // Get the rotation that player needs to be in.
        float rotation = Mathf.Atan2(mouseY - transform.position.y, mouseX - transform.position.x)*Mathf.Rad2Deg;
        // Set the rotation.
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        
    }
    
    void moveToward(Vector2 targetPosition, float speed){
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 positionDifference = targetPosition - currentPosition;  // Vector between current and target position.
        
        // If moving won't move past target position:
        if(positionDifference.magnitude > (positionDifference.normalized*speed).magnitude){
            transform.position = currentPosition + (targetPosition - currentPosition).normalized*speed;
        }
        else{
            transform.position = targetPosition;
        }
    }
}
