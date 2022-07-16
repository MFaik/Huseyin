using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public void Update() {
        OnUpdate();
    }
    public virtual void OnUpdate() { }

    public virtual IEnumerator NextStep() { yield return null; }

    public virtual IEnumerator Move(Vector2 tar) { yield return null;  }

    public virtual void Shoot(Vector2 spawn, Vector2 tar) { }

    public virtual void Die() { }
}
