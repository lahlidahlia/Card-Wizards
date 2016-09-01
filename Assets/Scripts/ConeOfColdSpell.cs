using UnityEngine;
using System.Collections;

public class ConeOfColdSpell : Spell {
    public override int cooldown {
        get { return 2; }
    }

    public ConeOfColdSpell(GM gm) : base(gm) {
        getAsset("ConeOfColdSpell");  // Find the assets placed on this object.
    }

    private float damage = 0.5f;

    public override void effect(GameObject player) {
        GameObject projectile = assets[0];
        if (projectile == null) {
            Debug.Log("NOTHING HAPPENED");
            return;
        }
        Object.Instantiate(projectile, player.transform.position, player.transform.rotation);
    }
}
