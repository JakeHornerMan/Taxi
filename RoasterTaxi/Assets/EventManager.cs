using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static readonly UIEvents uiEvents = new UIEvents();

    public class UIEvents
    {
        public UnityAction<Component, float> OnBoostChange;
    }
}
