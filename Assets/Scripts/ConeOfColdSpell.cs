using UnityEngine;
using System.Collections;

public class ConeOfColdSpell : Spell {
	public override float cooldown { get { return 2; } }
    public override float channelTime { get { return 2; } }

    public ConeOfColdSpell(GM gm) : base(gm) {
        getAsset("ConeOfColdSpell");  // Find the assets placed on this object.
    }

    public override void effect(GameObject player) {
        GameObject projectilePrefab = assets[0];
        if (projectilePrefab == null) {
            Debug.Log("NOTHING HAPPENED");
            return;
        }
        GameObject projectile = Object.Instantiate(projectilePrefab, player.transform.position, player.transform.rotation) as GameObject;
        projectile.AddComponent<ConeOfColdProjectile>();

        Player playerScr = player.GetComponent<Player>();
        playerScr.isChanneling = true;

		// Run the channeling effect.
		playerScr.castingCoroutine = channel(playerScr, projectile);
    }

    class ConeOfColdProjectile : PersistentProjectile {
        protected override float damage {
            get { return 0.1f; }  // Damage per second.
        }
    }
}
