using UnityEngine;
using System.Collections;

public class ConeOfColdSpell : Spell {
    public override int cooldown { get { return 2; } }
    public override bool isChannel { get { return true; } }

    public ConeOfColdSpell(GM gm) : base(gm) {
        getAsset("ConeOfColdSpell");  // Find the assets placed on this object.
    }

    private float damage = 0.5f;

    public override void effect(GameObject player) {
        GameObject projectilePrefab = assets[0];
        if (projectilePrefab == null) {
            Debug.Log("NOTHING HAPPENED");
            return;
        }
        GameObject projectile = Object.Instantiate(projectilePrefab, player.transform.position, player.transform.rotation) as GameObject;
        Player playerScr = player.GetComponent<Player>();
        playerScr.isChanneling = true;
        gm.RunCoroutine(channel(playerScr, projectile));
    }

    public IEnumerator channel(Player playerScr, GameObject projectile) {
        while(playerScr.isChanneling) {
            Debug.Log("CHANNELING!!!");
            yield return null;
        }
        Object.Destroy(projectile);
    }
}
