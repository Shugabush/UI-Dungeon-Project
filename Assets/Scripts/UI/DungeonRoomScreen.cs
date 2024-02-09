using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DungeonRoomScreen : MonoBehaviour
{
    [SerializeField] Canvas screenCanvas;
    [SerializeField] Image combatReport;
    [SerializeField] TMP_Text difficultyAndSuccessText;
    [SerializeField] TMP_Text successPercentText;
    [SerializeField] Button continueButton;
    [SerializeField] TMP_Text continueButtonText;
    [SerializeField] LootLayout loot;

    public static UnityEvent onDungeonEntered = new UnityEvent();
    public static UnityEvent onDungeonExited = new UnityEvent();

    public static Canvas ScreenCanvas => instance.screenCanvas;
    static Image CombatReport => instance.combatReport;
    static TMP_Text DifficultyAndSuccessText => instance.difficultyAndSuccessText;
    static TMP_Text SuccessPercentText => instance.successPercentText;
    static Button ContinueButton => instance.continueButton;
    static TMP_Text ContinueButtonText => instance.continueButtonText;
    static LootLayout Loot => instance.loot;

    static DungeonRoomScreen instance;

    void Awake()
    {
        instance = this;
        continueButton.onClick.AddListener(DisableCombatReport);
    }

    void OnDestroy()
    {
        onDungeonEntered.RemoveAllListeners();
        onDungeonExited.RemoveAllListeners();
    }

    public static void EnableCombatReport(DungeonRoom room)
    {
        onDungeonEntered.Invoke();

        ActiveCanvasManager.SetCanvasActive(ScreenCanvas);

        DungeonMap.SetCurrentDifficulty(room);

        CombatReport.enabled = true;

        int successChance = DungeonMap.CurrentDifficulty.baseFightSuccess + PlayerStatsScreen.GetAdditionalFightSuccess();

        if (GameManager.IsInvisible)
        {
            successChance = 0;
        }

        string successChanceString = successChance.ToString("00") + "%";

        int successPercent = Random.Range(0, 100);
        string successString = string.Empty;

        if (successPercent >= successChance)
        {
            // The player failed
            successString += "\nFAIL!";
            if (GameManager.IsInvisible)
            {
                successString += " (Invisible. No damage taken)";
            }
            Loot.gameObject.SetActive(false);
        }
        else
        {
            // The player succeeded
            successString += "\nSUCCESS!";
            Loot.gameObject.SetActive(true);

            Loot.GenerateLoot();
        }

        // The player completes the room regardless of whether they succeeded or not
        room.Completed = true;

        if (!GameManager.IsInvisible)
        {
            int targetDamage = Random.Range(DungeonMap.CurrentDifficulty.minHealthLost, DungeonMap.CurrentDifficulty.maxHealthLost) - GameManager.Armor;
            targetDamage = Mathf.Clamp(targetDamage, 1, targetDamage);

            GameManager.Health -= targetDamage;
        }

        DifficultyAndSuccessText.text = $"Difficulty: {room.DifficultyIndex}\nSuccess Chance: {successChanceString}";
        SuccessPercentText.text = successString;
        ContinueButton.image.enabled = true;
        ContinueButtonText.text = "Continue";

        onDungeonExited.Invoke();

        onDungeonEntered.RemoveAllListeners();
        onDungeonExited.RemoveAllListeners();
    }

    public static void DisableCombatReport()
    {
        ActiveCanvasManager.SetCanvasAndChildrenActive(DungeonMap.Screen);

        CombatReport.enabled = false;
        DifficultyAndSuccessText.text = string.Empty;
        SuccessPercentText.text = string.Empty;
        ContinueButton.image.enabled = false;
        ContinueButtonText.text = string.Empty;
    }
}
