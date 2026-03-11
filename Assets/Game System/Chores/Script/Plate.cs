using System;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private RectTransform centerPosition;
    [Header("Prefab")]
    [SerializeField] private PlateDirt plateDirtPrefab;
    [Header("Plate")]
    [SerializeField] private int plateDirtMinAmount = 3;
    [SerializeField] private int plateDirtMaxAmount = 5;
    [SerializeField] private float plateDirtSpawnRadius = 1f;
    [SerializeField] private int plateDirtMinThickness = 3;
    [SerializeField] private int plateDirtMaxThickness = 5;

    private int totalDirtThickness = 0;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(plateDirtPrefab, "plateDirtPrefab is empty");
    }
    #endregion

    // ====================================================================================================
    //                     Plate Functions
    // ====================================================================================================
    #region Plate
    public void GeneratePlateDirt()
    {
        // Get random number
        int randomAmount = UnityEngine.Random.Range(plateDirtMinAmount, plateDirtMaxAmount);
        // Spawn plate dirts number of times
        for(int index = 0; index < randomAmount; index++)
        {
            // Create instance
            PlateDirt plateDirtInstance = Instantiate(plateDirtPrefab);
            // Set position
            float randomRadius = UnityEngine.Random.Range(0f, plateDirtSpawnRadius);
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            Vector3 offsetPosition = new Vector3(randomRadius, 0, 0);
            offsetPosition = RotateVector2D(offsetPosition, randomAngle);
            plateDirtInstance.transform.position = centerPosition.transform.position + offsetPosition;
            // Set thickness
            int randomThickness = UnityEngine.Random.Range(plateDirtMinThickness, plateDirtMaxThickness);
            plateDirtInstance.Thickness = randomThickness;
            // Set parent
            plateDirtInstance.transform.SetParent(transform);
            // Connect events and update variables
            plateDirtInstance.OnPlateDirtCleaned += (object sender, EventArgs e) => totalDirtThickness--;
            totalDirtThickness++;
        }
    }
    #endregion

    // ====================================================================================================
    //                     Helper Functions
    // ====================================================================================================
    #region Helper
    public Vector3 RotateVector2D(Vector3 vector, float angleDegrees)
    {
        // Convert degrees to rad
        float rad = angleDegrees * Mathf.Deg2Rad;
        // Find sin and cos from angle
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        // Apply rotation to vector
        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;
        // Return rotated vector
        return new Vector3(x, y, vector.z);
    }
    #endregion
}
