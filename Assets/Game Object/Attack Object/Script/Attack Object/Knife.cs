using UnityEngine;

public class Knife : BaseAttackObject
{
    [Header("Spawn")]
    [SerializeField] private float minSpawnRangeY = -10f;
    [SerializeField] private float maxSpawnRangeY = 10f;

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
    #endregion
}
