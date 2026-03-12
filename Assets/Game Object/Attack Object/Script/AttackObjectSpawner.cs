using System.Collections.Generic;
using UnityEngine;

public class AttackObjectSpawner : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private Transform attackObjectSpawnPosition;
    [SerializeField] private Transform attackObjectParent;
    [Header("Attack Object")]
    [SerializeField] private List<BaseAttackObject> baseAttackObjectPrefabList;
    [Header("Spawn Settings")]
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 3f;
    [SerializeField] private float initialSpawnTime = 1.5f;

    // Spawn
    private bool isSpawnTimerActive = false;
    private float spawnTimer = 0f;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(attackObjectSpawnPosition, "attackObjectSpawnPosition is empty");
        Debug.Assert(attackObjectParent, "attackObjectParent is empty");
        Debug.Assert(baseAttackObjectPrefabList.Count > 0, "baseAttackObjectPrefabList is empty");
        // Initialize
        spawnTimer = initialSpawnTime;
    }

    private void Update()
    {
        // Update spawn timer
        if (isSpawnTimerActive)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnRandomAttackObject();
                spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }
    #endregion

    // ====================================================================================================
    //                     Spawn Functions
    // ====================================================================================================
    #region Spawn
    public void SpawnRandomAttackObject()
    {
        // Create a random instance
        int randomIndex = Random.Range(0, baseAttackObjectPrefabList.Count);
        BaseAttackObject attackObjectInstance = Instantiate(baseAttackObjectPrefabList[randomIndex]);
        // Initialize object
        attackObjectInstance.InitializeObject();
        // Set parent
        attackObjectInstance.transform.SetParent(attackObjectParent);
    }
    #endregion

    // ====================================================================================================
    //                     Timer Functions
    // ====================================================================================================
    #region Timer
    public void ResumeSpawnTimer(){isSpawnTimerActive = true;}

    public void PauseSpawnTimer(){isSpawnTimerActive = false;}
    #endregion
}
