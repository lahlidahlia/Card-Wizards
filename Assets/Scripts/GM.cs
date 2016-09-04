using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour {
    public GameObject[] gameObjectList;
    public Dictionary<int, Spell> spellDictionary;
    // Use this for initialization
    void Awake() {

    }
    void Start () {
        spellDictionary = new Dictionary<int, Spell>() {
            {1, new FireBlastSpell(this) {ID = 1} },
            {2, new ConeOfColdSpell(this) {ID = 2} },
            {3, new EarthDriveSpell(this) {ID = 3} },
        };
	}
	
    public Coroutine RunCoroutine(IEnumerator coroutine) {
        /* Because you can't run a coroutine from a non-monobehaviour script apparently*/
        return StartCoroutine(coroutine);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
