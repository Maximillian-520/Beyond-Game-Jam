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
        float flippedAngleX =  transform.eulerAngles.x;
        int randomNumber = UnityEngine.Random.Range(0, 2);
        if (randomNumber == 0) flippedAngleX *= -1;
        transform.eulerAngles = new Vector3(
            flippedAngleX,
            transform.eulerAngles.y,
            transform.position.z
        );
        // Play animation
    }
    #endregion
}
