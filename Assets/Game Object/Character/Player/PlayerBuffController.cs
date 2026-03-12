using UnityEngine;

public class PlayerBuffController : MonoBehaviour
{
    [Header("Component and Object")]
    [SerializeField] private PlayerControlState playerControlState;
    [SerializeField] private ParticleSystem smallSpeedBuffParticle;
    [SerializeField] private ParticleSystem bigSpeedBuffParticle;
    [SerializeField] private ParticleSystem smallSpeedDebuffParticle;
    [SerializeField] private ParticleSystem bigSpeedDebuffParticle;
    [Header("Speed")]
    [SerializeField] private float maxSpeedBuffAmount = 2.0f;
    [SerializeField] private float maxSpeedDebuffAmount = 2.0f;
    [SerializeField] [Range(0, 1)] private float speedBuffParticleThreshold = 0.5f;
    [SerializeField] [Range(0, 1)] private float speedDebuffParticleThreshold = 0.5f;

    // Speed
    private float speedEffectTimer = 0;
    private float speedOffsetAmount = 0;
    private ParticleSystem currentSpeedEffectParticle;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(playerControlState, "playerControlState is missing");
        Debug.Assert(smallSpeedBuffParticle, "smallSpeedBuffParticle is missing");
        Debug.Assert(bigSpeedBuffParticle, "bigSpeedBuffParticle is missing");
        Debug.Assert(smallSpeedDebuffParticle, "smallSpeedDebuffParticle is missing");
        Debug.Assert(bigSpeedDebuffParticle, "bigSpeedDebuffParticle is missing");
    }

    private void Update()
    {
        // Update speed effect timer
        if (speedEffectTimer > 0)
        {
            speedEffectTimer -= Time.deltaTime;
            if (speedEffectTimer <= 0) ClearSpeedEffect();
        }
    }
    #endregion

    // ====================================================================================================
    //                     Speed Functions
    // ====================================================================================================
    #region Speed
    public void GiveSpeedBuff(float valueNormalized, float duration)
    {
        // Check if another speed effect exist
        if (speedEffectTimer > 0) ClearSpeedEffect();
        // Set speed buff
        speedEffectTimer = duration;
        speedOffsetAmount = maxSpeedBuffAmount * Mathf.Clamp(valueNormalized, 0, 1);
        playerControlState.maxWalkSpeed += speedOffsetAmount;
        // Set particle
        if (valueNormalized < speedBuffParticleThreshold)
        {
            currentSpeedEffectParticle = smallSpeedBuffParticle;
        }
        else
        {
            currentSpeedEffectParticle = bigSpeedBuffParticle;
        }
        currentSpeedEffectParticle.Play();
    }

    public void GiveSpeedDebuff(float valueNormalized, float duration)
    {
        // Check if another speed effect exist
        if (speedEffectTimer > 0) ClearSpeedEffect();
        // Set speed debuff
        speedEffectTimer = duration;
        speedOffsetAmount = -maxSpeedDebuffAmount * Mathf.Clamp(valueNormalized, 0, 1);
        playerControlState.maxWalkSpeed += speedOffsetAmount;
        // Set particle
        if (valueNormalized < speedDebuffParticleThreshold)
        {
            currentSpeedEffectParticle = smallSpeedDebuffParticle;
        }
        else
        {
            currentSpeedEffectParticle = bigSpeedDebuffParticle;
        }
        currentSpeedEffectParticle.Play();
    }

    public void ClearSpeedEffect()
    {
        playerControlState.maxWalkSpeed -= speedOffsetAmount;
        speedOffsetAmount = 0;
        currentSpeedEffectParticle.Stop();
        currentSpeedEffectParticle = null;
    }
    #endregion
}
