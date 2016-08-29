using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Spell{
    public abstract int cooldown { get; }  // Cooldown of spell in seconds
    public abstract int ID { get; }

    public abstract void effect(GameObject player);

    protected GM gm;
    protected List<GameObject> assets = new List<GameObject>();  // Contains 

    public Spell(GM gm) {
        this.gm = gm;

        Debug.Log("Constructed");
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
