using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voodoo.Pattern;

public class Preloader : SingletonMB<Preloader>
{
    public static Action OnComplete;

    public Animator LoadingAnim;

    private AsyncOperation loadMainSceneOperation;

    private bool VSInit_OK;

    public string NextScene;

    public static ABTestConfig ABTestConfig;
    public static ShopConfig ShopConfig;
    public static CameraConfig CameraConfig;
    public static HouseLevelConfig HouseLevelConfig;
    public static OptimizationConfig OptimizationConfig;
    public static BonusLevelConfig BonusLevelConfig;
    public static TapControlConfig TapControlConfig;
    public static WallGemsConfig WallGemsConfig;
    public static OnboardingConfig OnboardingConfig;
    public static FSSoonerConfig FSSoonerConfig;
    public static CityBuilderConfig CityBuilderConfig;
    public static CityBuilderBalanceConfig CityBuilderBalanceConfig;
    public static DuelFightModeConfig DuelFightModeConfig;
    public static EnvironmentConfig EnvironmentConfig;
    public static LotteryRVConfig LotteryRVConfig;
    public static StartingPositionConfig StartingPositionConfig;
    public static MinecraftLevelConfig MinecraftLevelConfig;
    public static RailDigging RailDigging;
    public static ShowLevelAtBeginning ShowLevelAtBeginning;
    public static StartRVConfig StartRVConfig;
    public static MouthpiecesGameplay MouthpiecesGameplay;
    public static Transition Transition;
    public static DrawYourOwnLevel DrawYourOwnLevel;
    public static ChestRoomConfig ChestRoomConfig;
    public static EnhancedIncremental EnhancedIncremental;
    public static GemIAP GemIAP;
    public static BetterNoAds BetterNoAds;
    public static BetterUpgradeScaling BetterUpgradeScaling;
    public static DifferentHeads DifferentHeads;
    public static BetterWinScreen BetterWinScreen;
    public static ExcavationMode ExcavationMode;
    public static FlyingBonusLevel FlyingBonusLevel;
    public static NewGamePlus NewGamePlus;
    public static RenewableEnergyBooster RenewableEnergyBooster;
    public static UIRetake UIRetake;
    public static SkinsByRV SkinsByRV;

    // =========================================================
    // CUSTOM LOADING
    // =========================================================

    [Header("Custom Loading")]

    [Tooltip("Time for loading bar to smoothly travel from 0% to 90%.")]
    [SerializeField]
    private float mainFillDuration = 2.5f;

    [Tooltip("Time for loading bar to smoothly travel from 90% to 100%.")]
    [SerializeField]
    private float finalFillDuration = 0.4f;

    [Tooltip("Hold loading bar at 100% before calling the ad.")]
    [SerializeField]
    private float completedHoldTime = 0.2f;

    [Tooltip("Prevents long scene-loading frames from making the loading bar jump.")]
    [SerializeField]
    private float maxVisualDeltaTime = 0.033f;

    // CustomSaw reads this value.
    [HideInInspector]
    public float visualProgress = 0f;

    // =========================================================
    // AD BEFORE NEXT SCENE
    // =========================================================

    [Header("Ad Before Next Scene")]

    [Tooltip("Call CheckCpAd before activating the next scene.")]
    [SerializeField]
    private bool callAdBeforeNextScene = true;

    [Tooltip("Small realtime delay after CallAd() before activating GamePlay.")]
    [SerializeField]
    private float adOpenDelay = 0.35f;

    private bool adCalled = false;


    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        visualProgress = 0f;
        adCalled = false;

        /*
         * We now control the LoadingBar/Filler/Saw manually.
         * Prevent the old Animator from hiding, fading,
         * or changing the custom loading UI.
         */
        if (LoadingAnim != null)
        {
            LoadingAnim.enabled = false;
        }

        /*
         * Default scene.
         */
        if (string.IsNullOrEmpty(NextScene))
        {
            NextScene = "GamePlay";
        }

        StartCoroutine(LoadNextScene(NextScene));
    }


    // =========================================================
    // LOAD NEXT SCENE
    // =========================================================

    private IEnumerator LoadNextScene(string sceneName)
    {
        visualProgress = 0f;

        /*
         * Give PreLoader one proper rendered frame at 0%.
         * This prevents the first visible frame from already
         * appearing partially filled.
         */
        yield return null;
        yield return new WaitForEndOfFrame();

        /*
         * Keep background scene loading from unnecessarily
         * blocking the loading UI animation.
         */
        Application.backgroundLoadingPriority = ThreadPriority.Low;

        loadMainSceneOperation =
            SceneManager.LoadSceneAsync(
                sceneName,
                LoadSceneMode.Single
            );

        if (loadMainSceneOperation == null)
        {
            Debug.LogError(
                "[Preloader] Failed to start loading scene: "
                + sceneName
            );

            yield break;
        }

        /*
         * VERY IMPORTANT:
         *
         * GamePlay is allowed to load in memory,
         * but Unity is NOT allowed to switch scenes yet.
         */
        loadMainSceneOperation.allowSceneActivation = false;

        /*
         * Lowest async operation priority.
         */
        loadMainSceneOperation.priority = -1;


        // =====================================================
        // PHASE 1
        // VISUAL 0% -> 90%
        // =====================================================

        float timer = 0f;

        while (timer < mainFillDuration)
        {
            float dt = GetSafeVisualDelta();

            timer += dt;

            float normalized =
                Mathf.Clamp01(
                    timer /
                    Mathf.Max(0.01f, mainFillDuration)
                );

            /*
             * Smooth start + smooth finish.
             */
            float smooth =
                normalized * normalized *
                (3f - 2f * normalized);

            /*
             * Visual progress is intentionally independent
             * from AsyncOperation.progress.
             *
             * This prevents:
             *
             * 0% -> suddenly 50% / 80%
             *
             * when Unity loads the scene quickly.
             */
            visualProgress =
                Mathf.Lerp(
                    0f,
                    0.9f,
                    smooth
                );

            yield return null;
        }

        visualProgress = 0.9f;


        // =====================================================
        // WAIT UNTIL UNITY ACTUALLY FINISHES LOADING
        // =====================================================

        /*
         * With allowSceneActivation = false,
         * Unity normally stops at progress = 0.9.
         *
         * Keep the visible loading bar at 90%
         * until GamePlay is genuinely ready.
         */
        while (loadMainSceneOperation.progress < 0.9f)
        {
            visualProgress = 0.9f;

            yield return null;
        }


        // =====================================================
        // PHASE 2
        // VISUAL 90% -> 100%
        // =====================================================

        timer = 0f;

        while (timer < finalFillDuration)
        {
            float dt = GetSafeVisualDelta();

            timer += dt;

            float normalized =
                Mathf.Clamp01(
                    timer /
                    Mathf.Max(0.01f, finalFillDuration)
                );

            float smooth =
                normalized * normalized *
                (3f - 2f * normalized);

            visualProgress =
                Mathf.Lerp(
                    0.9f,
                    1f,
                    smooth
                );

            yield return null;
        }


        // =====================================================
        // EXACT 100%
        // =====================================================

        visualProgress = 1f;

        /*
         * Give CustomSaw at least one complete rendered frame
         * to reach the right/end position.
         */
        yield return null;
        yield return new WaitForEndOfFrame();


        // =====================================================
        // HOLD 100%
        // =====================================================

        if (completedHoldTime > 0f)
        {
            yield return new WaitForSecondsRealtime(
                completedHoldTime
            );
        }


        // =====================================================
        // CALL AD BEFORE ACTIVATING GAMEPLAY
        // =====================================================

        if (callAdBeforeNextScene && !adCalled)
        {
            adCalled = true;

            if (CheckCpAd.instance != null)
            {
                Debug.Log(
                    "[Preloader] Calling ad before next scene."
                );

                /*
                 * YOUR EXISTING AD CALL.
                 *
                 * CheckCpAd.cs is NOT modified.
                 */
                CheckCpAd.instance.CallAd();

                /*
                 * Give the ad SDK a small amount of realtime
                 * to open before GamePlay is activated.
                 *
                 * This does NOT use Time.timeScale.
                 */
                if (adOpenDelay > 0f)
                {
                    yield return new WaitForSecondsRealtime(
                        adOpenDelay
                    );
                }
            }
            else
            {
                Debug.LogWarning(
                    "[Preloader] CheckCpAd.instance is null. " +
                    "Continuing to next scene without ad."
                );
            }
        }


        // =====================================================
        // ACTIVATE GAMEPLAY
        // =====================================================

        /*
         * Loading is 100%.
         * Saw has reached the end.
         * Ad has been called.
         *
         * Now allow Unity to switch to GamePlay.
         */
        loadMainSceneOperation.allowSceneActivation = true;


        // =====================================================
        // WAIT FOR SCENE ACTIVATION
        // =====================================================

        while (!loadMainSceneOperation.isDone)
        {
            yield return null;
        }


        // =====================================================
        // COMPLETE
        // =====================================================

        OnComplete?.Invoke();
    }


    // =========================================================
    // SAFE VISUAL DELTA
    // =========================================================

    private float GetSafeVisualDelta()
    {
        /*
         * Scene loading can occasionally produce a very
         * long frame on mobile.
         *
         * Example:
         *
         * Time.unscaledDeltaTime = 0.40
         *
         * Without clamping, the progress bar can suddenly
         * jump a huge distance.
         *
         * Clamp only affects the visual loading animation.
         * It does NOT modify Time.timeScale or game physics.
         */
        return Mathf.Min(
            Time.unscaledDeltaTime,
            Mathf.Max(
                0.001f,
                maxVisualDeltaTime
            )
        );
    }
}