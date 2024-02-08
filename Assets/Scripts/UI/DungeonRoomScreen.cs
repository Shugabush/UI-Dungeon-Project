using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public static void EnableCombatReport(DungeonRoom room)
    {
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
            GameManager.Health -= Random.Range(DungeonMap.CurrentDifficulty.minHealthLost, DungeonMap.CurrentDifficulty.maxHealthLost);
        }

        DifficultyAndSuccessText.text = $"Difficulty: {room.DifficultyIndex + 1}\nSuccess Chance: {successChanceString}";
        SuccessPercentText.text = successString;
        ContinueButton.image.enabled = true;
        ContinueButtonText.text = "Continue";

        // Invisibility only lasts for one dungeon room
        GameManager.IsInvisible = false;
    }

    public static void DisableCombatReport()
    {
        ActiveCanvasManager.SetCanvasActive(DungeonMap.Screen);

        CombatReport.enabled = false;
        DifficultyAndSuccessText.text = string.Empty;
        SuccessPercentText.text = string.Empty;
        ContinueButton.image.enabled = false;
        ContinueButtonText.text = string.Empty;
    }
}
