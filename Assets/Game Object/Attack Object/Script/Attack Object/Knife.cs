using System;
using UnityEngine;

public class Knife : BaseAttackObject
{
    [Header("Component and Object")]
    [SerializeField] private Animator animator;
    [Header("Spawn")]
    [SerializeField] private float minSpawnRangeY = -10f;
    [SerializeField] private float maxSpawnRangeY = 10f;
    [Header("Animation")]
    [SerializeField] private string attackLeftAnimationName = "KnifeAttackLeft";
    [SerializeField] private string attackRightAnimationName = "KnifeAttackRight";

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(animator, "animator is missing");
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
            transform.position.x,
            UnityEngine.Random.Range(minSpawnRangeY, maxSpawnRangeY),
            transform.position.z
        );
        // Play animation
        int randomNumber = UnityEngine.Random.Range(0, 2);
        if (randomNumber == 0) animator.Play(attackLeftAnimationName);
        else animator.Play(attackRightAnimationName);
    }
    #endregion
}
