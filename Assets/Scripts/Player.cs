using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum PlayerState {
	FREE,
	CAST,
	EFFECT,
	CHANNEL,
	FINISH
}

public class Player : MonoBehaviour {

    public float speed;
    private Vector2 targetPosition;
    [System.NonSerialized]
    public bool isChanneling = false;

	public Text handText;
	public Text deckText;
	public Text cooldownText;
	public Text drawCooldownText;
	public Text channelText;
	public Text castText;

	private Deck deck;
	private Hand hand;

	private Timer cooldownTimer;  // Timer used for cooldown after casting a spell.
	private Timer drawTimer;  // Dictates when the player draws a card.
	public Timer channelTimer;  // For UI purpose only.
	private Timer castTimer;  // For UI purpose only.

	private float drawCooldown = 1;  // The delay between having an empty hand and drawing a card.

	private bool isCasting = false;  // The reason casting doesn't have a timer is because it uses coroutine instead.
	// Used for interrupting spells.
	public IEnumerator castingCoroutine;  // Coroutine that is casting the spell. Used in case player gets interrupted.
	private Spell castingSpell;
	private int castingKey ;  // The hand index that is being casted.



	public PlayerState currentState = PlayerState.FREE;
	public PlayerState lastState;

	void Start () {
		cooldownTimer = new Timer();
		drawTimer = new Timer();
		channelTimer = new Timer();
		castTimer = new Timer();
        deck = new Deck(new List<int>() { 1,1,1,2,2,2,3,3,3,3 });
		deck.Shuffle();
		hand = new Hand(deck, gameObject);
		while (hand.Draw()) { }  // Draw until hand is full.
	}
	
	// Update is called once per frame
	void Update () {
        // Get mouse coordinate.
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (Input.GetMouseButton(0)) {  // Left click.
			Interrupt();
        }
        if (Input.GetMouseButton(1)) {  // Right click to move.
            targetPosition = new Vector2(mouseX, mouseY);
        }

		// Key input detection.
		// Each key correspond to each hand slot.
		// -1 is no press.
		int inputKey = -1;  
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

		//if (lastState != currentState) {
		//	Debug.Log(currentState);
		//	lastState = currentState;
		//}

		if (currentState == PlayerState.FREE && inputKey != -1 && cooldownTimer.IsDone()) {
			// Store input and spell in a temp variable to be referenced throughout the process.
			castingKey = inputKey;
			castingSpell = hand.GetSpell(castingKey - 1);
			currentState = PlayerState.CAST;
		}
		if (currentState == PlayerState.CAST) {
			// Cast the 
			if (castingCoroutine == null) {
				castingCoroutine = Cast(castingSpell.castTime);
				StartCoroutine(castingCoroutine);
			}
			// State will be changed in the coroutine.
		}
		if (currentState == PlayerState.EFFECT) {
			castingSpell.effect(gameObject);
			currentState = PlayerState.CHANNEL;
		}
		if (currentState == PlayerState.CHANNEL) {
			if (castingSpell.channelTime == 0) {
				currentState = PlayerState.FINISH;
			}
			// For channeled spells:
			if (heldKey != castingKey) {
				currentState = PlayerState.FINISH;
			}
		}
		if (currentState == PlayerState.FINISH) {
			hand.Discard(castingKey - 1);
			cooldownTimer.Set(castingSpell.cooldown);
			castingKey = -1;
			castingSpell = null;
			castingCoroutine = null;
			currentState = PlayerState.FREE;

			// UI Only
			castTimer.Set(0);
			channelTimer.Set(0);
		}




  //      if (inputKey != -1) {  // When a spell key is being pressed (not held, to prevent repeat casting).
		//	// Cast a spell if not channeling and not on cooldown.
  //          if (!isChanneling && cooldownTimer.IsDone()
		//			&& !isCasting
		//			&& hand.GetSpell(inputKey - 1) != null) {
		//		currentlyCastingSlot = inputKey - 1;
		//		currentlyCastingSpell = hand.GetSpell(inputKey - 1);
		//		currentlyCastingCoroutine = StartCoroutine(CastSpell(inputKey - 1, hand.GetSpell(inputKey - 1).castTime));  // Start the casting coroutine.
		//	}
  //      } 
		//if(heldKey == -1){  // When no key is held (aka released).
  //          if (isChanneling == true) {  // When the player release key while channeling:
  //              isChanneling = false;
		//		cooldownTimer.Set(currentlyCastingSpell.cooldown);
  //          }
  //      }

        if (currentState == PlayerState.FREE) {
            lookAtMouse();
            moveToward(targetPosition, speed);
        }
		if (hand.GetFirstEmptySlot() != -1 && drawTimer.IsDone()) {  // if has a free slot and isn't in the process of drawing another one.
			System.Action f = () => { hand.Draw(); };
			drawTimer.Set(drawCooldown, f);  // That's a lambda. So that hand.Draw() doesn't return anything.
		}

		cooldownTimer.Tick();
		drawTimer.Tick();
		castTimer.Tick();

		// Graphically draw.
		handText.text = "Hand: " + hand.ToString();
		deckText.text = "Deck: " + deck.ToString();
		cooldownText.text = "Cooldown: " + cooldownTimer.GetTime().ToString();
		channelText.text = "Channel: " + channelTimer.GetTime().ToString();
		drawCooldownText.text = "Next Draw: " + drawTimer.GetTime().ToString();
		castText.text = "Cast: " + castTimer.GetTime().ToString();
	}

	//IEnumerator CastSpell(int index, float castTime) {
	//	/* Coroutine that casts a spell at the given hand index with a delay. */
	//	isCasting = true;
	//	yield return new WaitForSeconds(castTime);
	//	isCasting = false;
	//	Spell spellUsed = hand.GetSpell(index);
	//	cooldownTimer.Set(spellUsed.cooldown);  // Set player's cooldown.
	//	hand.Use(index);
	//}

	IEnumerator Cast(float castTime) {
		Debug.Log("CASTING!!!");
		castTimer.Set(castTime);
		yield return new WaitForSeconds(castTime);
		currentState = PlayerState.EFFECT;
		Debug.Log("FINISH CASTING!!!");
	}

	void Interrupt() {
		/* Interrupts the currently casting spell. */
		if (currentState == PlayerState.FREE) { return; }
		StopCoroutine(castingCoroutine);
		currentState = PlayerState.FINISH;
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
