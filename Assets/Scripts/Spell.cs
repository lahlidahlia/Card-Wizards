using UnityEngine;
using System.Collections;

public abstract class Spell {
    public abstract int cooldown { get; }  // Cooldown of spell in seconds
    public abstract int ID { get; }

    public abstract void effect(GameObject player);

    protected GM gm;

    public Spell(GM gm) {
        this.gm = gm;
        Debug.Log("Constructed");
    }
}
