using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICardGameElement
{
    Card Add(Card cardToAdd, int index);
    Card Remove(Card cardToRemove);
	
}
