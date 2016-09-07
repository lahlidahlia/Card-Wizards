using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell{
    public abstract float cooldown { get; }  // Time in seconds
	public virtual float castTime { get { return 0; } }  // Time in seconds
	public virtual bool isChannel { get { return false; } }  // Whether the spell is a channel spell.
    public int ID { get; set; }

    public abstract void effect(GameObject player);

    protected GM gm;
    protected List<GameObject> assets = new List<GameObject>();  // Contains 

    public Spell(GM gm) {
        this.gm = gm;
	}

    protected void getAsset(string name) {
		/*
		 *  Get assets from the associated gameObject's child.
		 */
        GameObject obj = GameObject.Find(name);
        foreach (Transform child in obj.transform) {
            assets.Add(child.gameObject);
        }
    }
}
