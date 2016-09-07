using UnityEngine;
using System.Collections;

public class FireBlastSpell : Spell {
    public override float cooldown { get { return 0.5f; } }

    private float damage = 1;
    private float projectileSpeed = 500;

    public FireBlastSpell(GM gm) : base(gm) {
        getAsset("FireBlastSpell");  // Find the assets placed on this object.
    }

    public override void effect(GameObject player){
        GameObject projectilePrefab = assets[0];
        if (projectilePrefab == null) {
            Debug.Log("NOTHING HAPPENED");
            return;
        }
        // Create the projectile.
        GameObject projectile = Object.Instantiate(projectilePrefab, player.transform.position, player.transform.rotation) as GameObject;
        projectile.AddComponent<FireBlastProjectile>();
        // Setting the projectile damage.
        projectile.GetComponent<FireBlastProjectile>().damage = damage;
        // Move the projectile.
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.AddForce(projectile.transform.right*projectileSpeed);
    }

    public class FireBlastProjectile : Projectile {

    }

}
