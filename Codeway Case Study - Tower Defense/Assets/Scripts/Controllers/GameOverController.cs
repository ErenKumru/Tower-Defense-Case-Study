using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverController : MonoBehaviour
{
    public Action OnLevelEndTriggered;


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        Monster monster = otherCollider.GetComponent<Monster>();

        if (monster != null)
        {
            OnLevelEndTriggered?.Invoke();
        }
    }
}
