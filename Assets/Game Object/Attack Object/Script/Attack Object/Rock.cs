using UnityEngine;

public class Rock : BaseAttackObject
{
    [Header("Component and Object")]
    [SerializeField] private DamageTriggerArea damageTriggerArea;
    [Header("Spawn")]
    [SerializeField] private float minSpawnRangeX = -10f;
    [SerializeField] private float maxSpawnRangeX = 10f;
    [SerializeField] private float minSpawnRangeY = -10f;
    [SerializeField] private float maxSpawnRangeY = 10f;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(damageTriggerArea, "damageTriggerArea is missing");
    }
    #endregion

    // ====================================================================================================
    //                     Attack Functions
    // ====================================================================================================
    #region Attack
    public override void InitializeObject()
    {
        // Set position
        transform.position = new Vector3(
            UnityEngine.Random.Range(minSpawnRangeX, maxSpawnRangeX),
            UnityEngine.Random.Range(minSpawnRangeY, maxSpawnRangeY),
            transform.position.z
        );
    }

    public void TriggerDamage() {damageTriggerArea.TriggerDamage();}
    #endregion
}
