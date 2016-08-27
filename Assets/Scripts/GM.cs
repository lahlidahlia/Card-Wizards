using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour {
    public GameObject[] gameObjectList;
    public Dictionary<int, Spell> spellDictionary = new Dictionary<int, Spell>();
	// Use this for initialization
	void Start () {
        Spell tempSpell = new FireBlastSpell(this);
        spellDictionary.Add(tempSpell.ID, tempSpell);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
