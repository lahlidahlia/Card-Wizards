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

    }

    public override void effect(GameObject player){
        GameObject[] list = gm.gameObjectList;
        GameObject spell = null;
        foreach (GameObject gameObject in list) {
            if (gameObject.name == "FireBlastSpell") {
                spell = gameObject;
                break;
            }
        }
        if (spell == null) {
            Debug.Log("NOTHING HAPPENED");
            return;
        }
        Object.Instantiate(spell, player.transform.position, player.transform.rotation);
    }


}
