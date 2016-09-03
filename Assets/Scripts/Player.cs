using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    private Vector2 targetPosition;
    private FireBlastSpell spell;
    private GM gm;
    [System.NonSerialized]
    public bool isChanneling = false;
	// Use this for initialization
	void Start () {
        gm = GameObject.Find("GM").GetComponent<GM>();
	}
	
	// Update is called once per frame
	void Update () {
        // Get mouse coordinate.
        float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        // Right click to move.
        if(Input.GetMouseButton(1)){
            targetPosition = new Vector2(mouseX, mouseY);
        }
        int inputKey = 0;
        if (Input.GetKey("1")) {
            inputKey = 1;
        }
        else if (Input.GetKey("2")) {
            inputKey = 2;
        }
        if (inputKey != 0) {  // When a spell key is being pressed.
            Spell spell = gm.spellDictionary[inputKey];  // Get the spell being casted.
            if (!isChanneling) {
                spell.effect(gameObject);  // Run the spell's effect if the player isn't already channeling something.
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
