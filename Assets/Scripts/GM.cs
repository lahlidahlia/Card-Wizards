﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour {
    public GameObject[] gameObjectList;
    public Dictionary<int, Spell> spellDictionary;
    // Use this for initialization
    void Start () {
        spellDictionary = new Dictionary<int, Spell>() {
            {1, new FireBlastSpell(this) {ID = 1} },
            {2, new ConeOfColdSpell(this) {ID = 2} },
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
