using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager
{

    private Dictionary<Type, HotGirlEvent.Handler> _registeredHandlers = new Dictionary<Type, HotGirlEvent.Handler>();

    public void Register<T>(HotGirlEvent.Handler handler) where T : HotGirlEvent
    {
        var type = typeof(T);
        if (_registeredHandlers.ContainsKey(type))
        {
            if (!IsEventHandlerRegistered(type, handler))
                _registeredHandlers[type] += handler;
        }
        else
        {
            _registeredHandlers.Add(type, handler);
        }
    }

    public void Unregister<T>(HotGirlEvent.Handler handler) where T : HotGirlEvent
    {
        var type = typeof(T);
        if (!_registeredHandlers.TryGetValue(type, out var handlers)) return;

        handlers -= handler;

        if (handlers == null)
        {
            _registeredHandlers.Remove(type);
        }
        else
        {
            _registeredHandlers[type] = handlers;
        }
    }

    public void Fire(HotGirlEvent e)
    {
        var type = e.GetType();

        if (_registeredHandlers.TryGetValue(type, out var handlers))
        {
            handlers(e);
        }
    }

    public bool IsEventHandlerRegistered(Type typeIn, Delegate prospectiveHandler)
    {
        return _registeredHandlers[typeIn].GetInvocationList().Any(existingHandler => existingHandler == prospectiveHandler);
    }
}

public abstract class HotGirlEvent
{
    public readonly float creationTime;

    public HotGirlEvent()
    {
        creationTime = Time.time;
    }

    public delegate void Handler(HotGirlEvent e);
}

//May be unnecessary now
public class PlayerTurnBegan : HotGirlEvent
{

}
//IMPORTANT
public class ActionCardPlayed : HotGirlEvent
{
    public Card cardPlayed;

    public ActionCardPlayed(Card someCard)
    {
        this.cardPlayed = someCard;
    }
}

//Event for NPC actions
public class NPCTakesAction : HotGirlEvent
{
    public NPC npcThatActed;

    public NPCTakesAction(NPC currentNPC)
    {
        npcThatActed = currentNPC;
    }
}

//May be unnecessary now
public class PlayerInputRequested : HotGirlEvent
{

}

//Unused for time being
public class CardDiscarded : HotGirlEvent
{

}
