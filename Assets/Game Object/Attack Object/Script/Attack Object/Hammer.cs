using System;
using UnityEngine;

public class Hammer : BaseAttackObject
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
            Mathf.Clamp(Player.Instance.transform.position.x, minSpawnRangeX, maxSpawnRangeX),
            Mathf.Clamp(Player.Instance.transform.position.y, minSpawnRangeY, maxSpawnRangeY),
            transform.position.z
        );
        // Set rotation
        float flippedScaleX =  transform.localScale.x;
        int randomNumber = UnityEngine.Random.Range(0, 2);
        if (randomNumber == 0) flippedScaleX *= -1;
        transform.localScale = new Vector3(
            flippedScaleX,
            transform.localScale.y,
            transform.localScale.z
        );
    }

    public void TriggerDamage() {damageTriggerArea.TriggerDamage();}
    #endregion
}
