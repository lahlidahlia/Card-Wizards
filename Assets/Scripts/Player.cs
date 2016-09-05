using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;
    private Vector2 targetPosition;
    private FireBlastSpell spell;
    [System.NonSerialized]
    public bool isChanneling = false;

    private Deck deck;
	private Hand hand;

	public Text handText;
	public Text deckText;
	// Use this for initialization
	void Start () {
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
        int inputKey = 0;  // 0 is no press.
        if (Input.GetKeyDown("1")) {
            inputKey = 1;
        } else if (Input.GetKeyDown("2")) {
            inputKey = 2;
        } else if (Input.GetKeyDown("3")) {
            inputKey = 3;
        } else if (Input.GetKeyDown("4")) {
            inputKey = 4;
        }
        if (inputKey == 5) {
			Debug.Log(deck.ToString());
        }
        if (inputKey != 0 && inputKey != 5) {  // When a spell key is being pressed.
            if (!isChanneling) {  // Prevent player from casting again if player is channeling.
				hand.Use(inputKey - 1, gameObject);
				hand.Draw();
            }
        } else {  // When no key is pressed.
            if (isChanneling == true) {  // When the player release key while channeling:
                isChanneling = false;
            }
        }

        if (!isChanneling) {
            lookAtMouse();
            moveToward(targetPosition, speed);
        }

		// Draw hand.
		handText.text = hand.ToString();
		deckText.text = deck.ToString();
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
