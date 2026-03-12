using UnityEngine;

public class Hammer : BaseAttackObject
{
    [Header("Component and Object")]
    [SerializeField] private Animator animator;
    [SerializeField] private DamageTriggerArea damageTriggerArea;
    [Header("Spawn")]
    [SerializeField] private float minSpawnRangeX = -10f;
    [SerializeField] private float maxSpawnRangeX = 10f;
    [SerializeField] private float minSpawnRangeY = -10f;
    [SerializeField] private float maxSpawnRangeY = 10f;
    [Header("Animation")]
    [SerializeField] private string attackLeftAnimationName = "HammerAttackLeft";
    [SerializeField] private string attackRightAnimationName = "HammerAttackRight";

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(animator, "animator is missing");
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
        // Play animation
        int randomNumber = UnityEngine.Random.Range(0, 2);
        if (randomNumber == 0) animator.Play(attackLeftAnimationName);
        else animator.Play(attackRightAnimationName);
    }

    public void TriggerDamage() {damageTriggerArea.TriggerDamage();}
    #endregion
}
