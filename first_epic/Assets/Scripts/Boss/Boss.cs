using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Boss : MonoBehaviour
{
    protected BossStageTimer _timer;

    private Scrollbar _scrollBar;                                  
    private BossStageSystem _stageSystem;

    public void InitBase(Scrollbar bar)
    {
        _scrollBar = bar;

        _timer = new BossStageTimer(3.0f);
        _stageSystem = new BossStageSystem();
        PrepareListeners();
    }

    private void PrepareListeners()
    {
        _stageSystem.SubscribeStageActions(new System.Action[] {
            OnFirstStage, OnSecondStage, OnThirdStage
        });

        _stageSystem.SubscribeStagePrepareActions(new System.Action[] {
            OnFirstPrepare, OnSecondPrepare, OnThirdPrepare
        });

        _timer.SubscribeAction(_stageSystem.SwitchStage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BossPlayer bossPlayer = collision.gameObject.GetComponent<BossPlayer>();
        if (bossPlayer != null)
            SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        _timer.UnsubscribeAction(_stageSystem.SwitchStage);
    }

    private void Update()
    {
        _timer.UpdateTimer();
        _stageSystem.ActivateStageAction();
        _scrollBar.size = _timer.CurrentTimer / _timer.TimerLimit;
    }

    // Stage
    protected abstract void OnFirstStage();
    protected abstract void OnSecondStage();
    protected abstract void OnThirdStage();

    // Preparation
    protected abstract void OnFirstPrepare();
    protected abstract void OnSecondPrepare();
    protected abstract void OnThirdPrepare();
}
