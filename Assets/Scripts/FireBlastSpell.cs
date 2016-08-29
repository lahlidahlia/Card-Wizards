using UnityEngine;
using System.Collections;

public class FireBlastSpell : Spell {
    public override int cooldown{
        get{ return 1; }
    }
    public override int ID {
        get { return 1; }
    }

    public FireBlastSpell(GM gm) : base(gm) {
        getAsset("FireBlastSpell");  // Find the assets placed on this object.
    }

    public override void effect(GameObject player){
        GameObject projectile = assets[0];
        if (projectile == null) {
            Debug.Log("NOTHING HAPPENED");
            return;
        }
        Object.Instantiate(projectile, player.transform.position, player.transform.rotation);
    }


}
