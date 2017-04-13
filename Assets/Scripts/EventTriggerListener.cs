using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class EventTriggerListener : UnityEngine.EventSystems.EventTrigger {

	public delegate void VoidDelegate (GameObject go);
    public VoidDelegate OnEnter;
    public VoidDelegate OnExit;
    public VoidDelegate OnPress;

    public static EventTriggerListener Get(GameObject go)
    {
        EventTriggerListener listener = go.GetComponent<EventTriggerListener>();
        if (listener == null)
            listener = go.AddComponent<EventTriggerListener>();
        return listener;
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (OnEnter != null)
            OnEnter(gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (OnExit != null)
            OnExit(gameObject);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (OnPress != null)
            OnPress(gameObject);
    }
}
