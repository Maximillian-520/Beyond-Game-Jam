using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageTriggerArea : MonoBehaviour
{
    public bool isActive = false;

    private Collider2D currentCollider;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Get collider
        currentCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check is active
        if (!isActive) return;
        // Try call receive damage
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (!damageable.IsUnityNull()) damageable.ReceiveDamage(1);
    }
    #endregion

    // ====================================================================================================
    //                     Trigger Functions
    // ====================================================================================================
    #region Trigger
    public void TriggerDamage()
    {
        // Check is collided with player
        if (!Player.Instance) return;
        if (currentCollider.IsTouching(Player.Instance.GetComponent<Collider2D>()))
        {
            // Give damage
            Player.Instance.ReceiveDamage(1);
        }
    }
    #endregion
}
