using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField] private float radius = 0.1f;
    [SerializeField] private string collisionTag = "Ground";

    public bool isColliding {private set; get;}

    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius);
        if(!collider) isColliding = false;
        else{isColliding = collider.gameObject.CompareTag(collisionTag);}
    }

    public Collider2D GetCollidedCollider()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius);
        if(collider) return collider;
        else return null;
    }

    public void EnableChecking()
    {
        enabled = true;
    }

    public void DisableChecking()
    {
        enabled = false;
        isColliding = false;
    }
}
