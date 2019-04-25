using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour
{
    
    public float maxHitPoints;
    public float startingHitPoints;

    public virtual void killCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void resetCharacter();

    public abstract IEnumerator damageCharacter(int damage, float interval);
}
