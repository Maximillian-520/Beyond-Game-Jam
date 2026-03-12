using UnityEngine;

public abstract class BaseAttackObject : MonoBehaviour
{
    public abstract void InitializeObject();

    public void DespawnObject() {Destroy(gameObject);}
}
