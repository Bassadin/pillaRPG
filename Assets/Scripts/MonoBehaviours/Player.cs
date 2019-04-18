using UnityEngine;
public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    private void Start()
    {
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
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
                        shouldDisappear = true;
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
}
