using System;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public event EventHandler OnPlayerDied;

    public static Player Instance {private set; get;}

    [Header("Component and Object")]
    [SerializeField] private StateController stateController;
    [SerializeField] private Transform centerPosition;
    public PlayerSprite playerSprite;
    public PlayerBuffController playerBuffController;

    public bool Active
    {
        set
        {
            isActive = value;
            if (isActive) stateController.ChangeState(BaseState.StateName.PLAYER_CONTROL);
            else stateController.ChangeState(BaseState.StateName.IDLE);
        }
        get{return isActive;}
    }

    private bool isActive = true;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start()
    {
        // Assertion Check
        Debug.Assert(stateController, "stateController is missing");
        Debug.Assert(playerSprite, "playerSprite is missing");
        Debug.Assert(centerPosition, "centerPosition is missing");
        Debug.Assert(playerBuffController, "playerBuffController is missing");
    }
    #endregion

    // ====================================================================================================
    //                     Transform Functions
    // ====================================================================================================
    #region Transform
    public Vector3 GetCenterPosition() {return centerPosition.transform.position;}

    #endregion

    // ====================================================================================================
    //                     Health Functions
    // ====================================================================================================
    #region Health
    public void ReceiveDamage(int damageAmount)
    {
        // Check is active
        if (!Active) return;
        // Do character die
        CharacterDie();
    }

    public void ReceiveHeal(int healAmount){}

    private void CharacterDie()
    {
        Debug.Log("player dead");
        OnPlayerDied.Invoke(this, EventArgs.Empty);
    }
    #endregion
}
