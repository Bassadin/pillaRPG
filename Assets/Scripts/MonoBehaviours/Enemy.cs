using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    float hitPoints;
    public int damageStrength;
    Coroutine damageCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
        }
    }
    public override IEnumerator damageCharacter(int damage, float interval)
    {
        while(true)
        {
            hitPoints -= damage;

            if (hitPoints <= float.Epsilon)
            {
                killCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                break;
            }
        }
    }

    public override void resetCharacter()
    {
        hitPoints = startingHitPoints;
    }

    private void OnEnable()
    {
        resetCharacter();
    }
}
