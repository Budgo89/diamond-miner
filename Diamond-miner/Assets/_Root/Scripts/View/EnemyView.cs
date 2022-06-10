using System;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    public Action<MonoBehaviour> EnemyContact { get; set; }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var levelObject = collider.gameObject.GetComponent<EnemyView>();
        EnemyContact?.Invoke(levelObject);
    }
}
