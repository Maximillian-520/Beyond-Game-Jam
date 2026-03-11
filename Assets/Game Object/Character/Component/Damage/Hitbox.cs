using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hitbox : MonoBehaviour
{
    [SerializeField] private GameObject targetDamageable;
    public IDamageable damageable {private set; get;}

    private void Start()
    {
        // Check and set damageable
        if (!targetDamageable)
        {
            Debug.Log("targetDamageable is missing");
            return;
        }
        damageable = targetDamageable.GetComponent<IDamageable>();
        if (damageable.IsUnityNull()){
            Debug.Log("targetDamageable is does not have IDamageable interface");
            return;
        }
    }
}
