using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    private Vector2 targetPosition;
    private FireBlastSpell spell;
    private GM gm;
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
        if (Input.GetKey("1")) {
            gm.spellDictionary[1].effect(gameObject);
        }
        if (Input.GetKey("2")) {
            gm.spellDictionary[2].effect(gameObject);
        }
        lookAtMouse();
        moveToward(targetPosition, speed);
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
