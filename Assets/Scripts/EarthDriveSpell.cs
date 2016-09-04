using UnityEngine;
using System.Collections;

public class EarthDriveSpell : Spell {
    public override int cooldown { get { return 2; } }
    public override bool isChannel { get { return true; } }

    private float spawnDelay = 0.5f;  // Delay between each projectile spawn.
    private float distanceBetweenEachProjectile = 1;  // In world coordinate, distance between each projectile.

    public EarthDriveSpell(GM gm) : base(gm) {
        getAsset("EarthDriveSpell");  // Find the assets placed on this object.
    }

    public override void effect(GameObject player) {
        GameObject projectilePrefab = assets[0];
        if (projectilePrefab == null) {
            Debug.Log("NOTHING HAPPENED");
            return;
        }

        // Run the channeling effect.
        gm.RunCoroutine(run(player, projectilePrefab));
    }

    public IEnumerator run(GameObject player, GameObject projectilePrefab) {
        GameObject lastProjectile = null;
        Vector2 position = player.transform.position;
        Quaternion direction = player.transform.rotation;
        while (true) {
            GameObject projectile = Object.Instantiate(projectilePrefab, position, direction) as GameObject;
            projectile.AddComponent<EarthDriveProjectile>();
            if(lastProjectile != null) {
                Object.Destroy(lastProjectile);
            }
            lastProjectile = projectile;
            float angleRadians = direction.eulerAngles.z * Mathf.Deg2Rad;  // Convert the direction to radians. Also a shortened variable.
            position = position + new Vector2(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians)).normalized * distanceBetweenEachProjectile;
            Debug.Log(direction.eulerAngles.z);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    class EarthDriveProjectile : MonoBehaviour {
        /* Earth drive projectile damages once per projectile created. */
        protected float damage {
            get { return 10; }
            set { damage = value; }
        }
        private bool alreadyDamaged = false;

        void OnCollisionEnter2D(Collision2D col) {
            if(col.gameObject.tag == "Enemy" && !alreadyDamaged) {
                Enemy enemyScript = col.gameObject.GetComponent<Enemy>();
                enemyScript.health -= damage;
                alreadyDamaged = true;
            }
        }
    }
}
