using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand {
	private int[] hand = new int[4];
	private Deck deck;
    public Hand(Deck deck) {
	/* Deck: the deck that this hand draws from*/
		this.deck = deck;
		for (int i = 0; i < hand.Length; i++) {
			hand[i] = -1;  // -1 represents empty hand.
		}
    }

    public bool Draw() {
	/*
	 * Add a card to the hand.
	 * Returns true if successful, false if otherwise, i.e. hand is full.
	 */
		for(int i = 0; i < hand.Length; i++) {
			if(hand[i] == -1) {  // If empty.
				hand[i] = deck.Draw();
				return true;  // Success.
			}
		}
		return false;  // Hand is full.
    }

	public void Use(int index, GameObject player) {
		/* Use the card at the given index. */
		int card = hand[index];
		if (card == -1) {  // If the hand slot is empty.
			return;
		}
		GM.spellDictionary[card].effect(player);
		deck.AddToDiscard(card);  // Add to discard pile to be reshuffled.
		hand[index] = -1;  // Remove the card from hand.		
	}

	public override string ToString() {
	/* Returns a string that represents the cards in hand. */
		string ret = "";
		foreach (int i in hand) {
			ret += i.ToString() + ", ";
		}
		return ret;
	}
}
