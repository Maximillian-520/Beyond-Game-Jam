using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public event EventHandler OnBattleTimerFinished;

    [SerializeField] private float battleTime = 60.0f;

    private bool isBattleTimerActive = false;
    private float battleTimer = 0.0f;
    
    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Update()
    {
        // Update battle timer
        if (isBattleTimerActive)
        {
            battleTimer -= Time.deltaTime;
            if (battleTimer <= 0)
            {
                OnBattleTimerFinished.Invoke(this, EventArgs.Empty);
                EndBattleTimer();
            }
        }
    }
    #endregion

    // ====================================================================================================
    //                     Timer Functions
    // ====================================================================================================
    #region Timer
    public void StartBattleTimer()
    {
        battleTimer = battleTime;
        isBattleTimerActive = true;
    }

    public void EndBattleTimer()
    {
        isBattleTimerActive = false;
    }
    #endregion
}
