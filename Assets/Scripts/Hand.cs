using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand {
	private int[] hand = new int[4];
	private Deck deck;  // The deck that this hand draws from.
	private GameObject player;  // The player object that own's this hand.
    public Hand(Deck deck, GameObject player) {
		/* Deck: the deck that this hand draws from*/
		this.deck = deck;
		this.player = player;
		// Initializes the hand as empty.
		for (int i = 0; i < hand.Length; i++) {
			hand[i] = -1;  // -1 represents empty hand.
		}
    }

	public int GetFirstEmptySlot() {
		/* Returns the first empty slot if there is an empty hand slot, otherwise returns -1. */
		for (int i = 0; i < hand.Length; i++) {
			if (hand[i] == -1) {
				return i;
			}
		}
		return -1;
	}
    public bool Draw() {
		/*
		 * Add a card to the hand.
		 * Returns true if successful, false if otherwise, i.e. hand is full.
		 */
		if (GetFirstEmptySlot() != -1) {
			hand[GetFirstEmptySlot()] = deck.Draw();
			return true;
		} else {
			return false;  // Hand is full.
		}
    }

	public void Discard(int index) {
		/* Discard a card at the given index. */
		Debug.Log(index);
		if (hand[index] != -1) {  // If the hand slot isn't empty.
			deck.AddToDiscard(hand[index]);  // Add to discard pile to be reshuffled.
			hand[index] = -1;  // Remove the card from hand.
		}
	}

	public Spell GetSpell(int index) {
		/* Returns the spell card at the given index.*/
		int card = hand[index];
		if (card == -1) {  // If the hand slot is empty.
			return null;
		}
		return GM.spellDictionary[card];
	}

	public void Use(int index) {
		/* Use the card at the given index. Also discards the card.*/
		GetSpell(index).effect(player);
		Discard(index);
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
