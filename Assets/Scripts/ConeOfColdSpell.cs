using UnityEngine;
using System.Collections;

public class ConeOfColdSpell : Spell {
    public override int cooldown { get { return 2; } }
    public override bool isChannel { get { return true; } }

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
        gm.RunCoroutine(channel(playerScr, projectile));
    }

    public IEnumerator channel(Player playerScr, GameObject projectile) {
        while(playerScr.isChanneling) {
            yield return null;
        }
        Object.Destroy(projectile);
    }

    class ConeOfColdProjectile : PersistentProjectile {
        protected override float damage {
            get {
                return 0.1f;  // Damage per second.
            }

            set {
                base.damage = value;
            }
        }
    }
}
