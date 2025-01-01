using BepInEx.Configuration;
using UnityEngine;

namespace AutoPickupTent
{
    internal static class AutoPickupTentConfig
    {
        internal static ConfigEntry<bool> EnableAutoPickup;
        internal static ConfigEntry<bool> EnableDialog;
        internal static ConfigEntry<bool> EnableNotifications;
        internal static ConfigEntry<KeyboardShortcut> FindTentsKey;
        internal static ConfigEntry<KeyboardShortcut> EmergencyPickupTentKey;
        
        internal static void LoadConfig(ConfigFile config)
        {
            EnableAutoPickup = config.Bind(
                section: ModInfo.Name,
                key: "Enable Auto Pickup",
                defaultValue: true,
                description: "Enable or disable the automatic pickup of tents.\n" +
                             "Set to 'true' to automatically pick up tents, or 'false' to disable the feature.\n" +
                             "テントを自動的に回収する機能を有効または無効にします。\n" +
                             "'true' に設定するとテントが自動的に回収され、'false' に設定するとこの機能が無効になります。\n" +
                             "启用或禁用帐篷的自动拾取功能。\n" +
                             "设置为 'true' 以自动拾取帐篷，或设置为 'false' 禁用该功能。"
            );
            
            EnableDialog = config.Bind(
                section: ModInfo.Name,
                key: "Enable Dialog",
                defaultValue: true,
                description: "Enable or disable the dialog prompt before automatically picking up tents.\n" +
                             "Set to 'true' to show a dialog confirmation, or 'false' to skip the prompt.\n" +
                             "テントを自動的に回収する前にダイアログプロンプトを表示するかどうかを設定します。\n" +
                             "'true' に設定すると確認ダイアログが表示され、'false' に設定するとプロンプトがスキップされます。\n" +
                             "启用或禁用在自动拾取帐篷之前的对话框提示。\n" +
                             "设置为 'true' 以显示确认对话框，或设置为 'false' 跳过提示。"
            );
            
            EnableNotifications = config.Bind(
                section: ModInfo.Name,
                key: "Enable Notifications",
                defaultValue: true,
                description: "Enable or disable notifications when tents are left behind.\n" +
                             "Set to 'true' to show notifications, or 'false' to disable them.\n" +
                             "テントを置き忘れた際の通知を有効または無効にします。\n" +
                             "'true' に設定すると通知が表示され、'false' に設定すると通知が無効になります。\n" +
                             "启用或禁用帐篷遗留时的通知。\n" +
                             "设置为 'true' 以显示通知，或设置为 'false' 禁用通知。"
            );
            
            FindTentsKey = config.Bind(
                section: ModInfo.Name,
                key: "Find Tents Key",
                defaultValue: new KeyboardShortcut(mainKey: KeyCode.F, modifiers: KeyCode.RightControl),
                description: "Key to find tents across all zones and display a notification.\n" +
                             "Press this key to locate tents left in zones and display a message.\n" +
                             "すべてのゾーンでテントを検索し、通知を表示するキーを設定します。\n" +
                             "このキーを押すと、ゾーンに置き去りにされたテントを見つけてメッセージを表示します。\n" +
                             "设置用于在所有区域中查找帐篷并显示通知的键。\n" +
                             "按下此键以定位留在区域中的帐篷并显示消息。"
            );
            
            EmergencyPickupTentKey = config.Bind(
                section: ModInfo.Name,
                key: "Emergency Pickup Tent Key",
                defaultValue: new KeyboardShortcut(mainKey: KeyCode.P, modifiers: KeyCode.RightControl),
                description: "Key to trigger emergency tent pickup across all locations in-game.\n" +
                             "Press this key to search all locations and automatically pick up all tents.\n" +
                             "ゲーム内のすべての場所で緊急テント回収をトリガーするキーを設定します。\n" +
                             "このキーを押すと、すべての場所を検索して、すべてのテントを自動的に回収します。\n" +
                             "设置触发在所有位置进行紧急帐篷拾取的按键。\n" +
                             "按下此键可搜索所有位置并自动拾取所有帐篷。\n"
            );
        }
    }
}