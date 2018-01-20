﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publisher : MonoBehaviour {

    List<GameObject> subscribers = new List<GameObject>();

    public void register(GameObject subscriber)
    {
        subscribers.Add(subscriber);
    }

	protected void Broadcast(string methodName, object data)
    {
        foreach (GameObject subscriber in subscribers)
            subscriber.SendMessage(methodName, data);
    }
}
