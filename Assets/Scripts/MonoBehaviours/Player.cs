using UnityEngine;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickups"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                print("Hit: " + hitObject.objectName);
                collision.gameObject.SetActive(false);
            }

        }
    }
}
