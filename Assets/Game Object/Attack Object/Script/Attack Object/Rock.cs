using System.Collections.Generic;
using UnityEngine;

public class Rock : BaseAttackObject
{
    [Header("Component and Object")]
    [SerializeField] private DamageTriggerArea damageTriggerArea;
    [SerializeField] private List<ParticleSystem> particleList;
    [Header("Spawn")]
    [SerializeField] private float minSpawnRangeX = -10f;
    [SerializeField] private float maxSpawnRangeX = 10f;
    [SerializeField] private float minSpawnRangeY = -10f;
    [SerializeField] private float maxSpawnRangeY = 10f;
    [Header("SFX")]
    [SerializeField] private List<string> sfxNameList;

    private int sfxPlayCount = 0;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(damageTriggerArea, "damageTriggerArea is missing");
        Debug.Assert(particleList.Count > 0, "particleList is missing");
        Debug.Assert(sfxNameList.Count > 0, "sfxNameList is missing");
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

    public void TriggerDamage()
    {
        damageTriggerArea.TriggerDamage();
        if (sfxPlayCount >= sfxNameList.Count)
        {
            AudioManager.Instance.PlaySFX(sfxNameList[sfxPlayCount - 1]);
        }
        else AudioManager.Instance.PlaySFX(sfxNameList[sfxPlayCount]);
        sfxPlayCount++;
    }

    public void TriggerParticle()
    {
        foreach(ParticleSystem particle in particleList) particle.Play();
    }
    #endregion
}
