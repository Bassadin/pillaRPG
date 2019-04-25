using System.Collections;
using UnityEngine;
public class Player : Character
{
    public HealthBar healthBarPrefab;
    public Inventory inventoryPrefab;
    public HitPoints hitPoints;

    HealthBar healthBar;
    Inventory inventory;

    public void Start()
    {
        
    }

    private void OnEnable()
    {
        ResetCharacter();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickups"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;
            if (hitObject != null)
            {
                bool shouldDisappear = false;
                print("Hit: " + hitObject.objectName);

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.addItem(hitObject);
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = adjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }

        }
    }

    public bool adjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value += amount;
            print("Adjusted hitpoints by " + amount + ". New Value: " + hitPoints.value + ".");
            return true;
        }
        return false;
    }

    public override void resetCharacter()
    {
        inventory = Instantiate(inventoryPrefab);
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;

        hitPoints.value = startingHitPoints;
    }

    public override void killCharacter()
    {
        base.killCharacter();

        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override IEnumerator damageCharacter(int damage, float interval)
    {
        while(true)
        {
            hitPoints.value -= damage;

            if (hitPoints.value <= float.Epsilon)
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
}
