using System;
using CrowdControl.Common;
using CrowdControl.Games.Packs;
using ConnectorType = CrowdControl.Common.ConnectorType;

public class Subnautica : SimpleTCPPack
{
    public override string Host => "127.0.0.1";

    public override ushort Port => 2679;

    public Subnautica(UserRecord player, Func<CrowdControlBlock, bool> responseHandler, Action<object> statusUpdateHandler) : base(player, responseHandler, statusUpdateHandler) { }

    public override Game Game => new Game("Subnautica_Below_Zero", "Subnautica_Below_Zero", "PC", ConnectorType.SimpleTCPServerConnector);

    public override EffectList Effects => new EffectList
    {
        new Effect("Rip Robin", "kill"),
        new Effect("Heal Robin", "heal"),
        new Effect("Toggle Day/Night", "toggle_day_night"),
        new Effect("Open PDA", "open_pda"),
        new Effect("Turn on the big gun", "gun"),
        new Effect("Fill Oxygen", "fill_o2"),
        new Effect("Drop Lifepod", "drop_lifepod"),
        new Effect("Cow or Shrimp? Yes.", "spawn_random_creature"),
        new Effect("Fill him up with junk", "junk"),
        new Effect("Get your pet shrimp to hang out", "reaper"),
        new Effect("Resource Roulette", "resource_roulette"),
        new Effect("Blueprint Roulette", "blueprint_roulette"),
        new Effect("An early breakfast", "eat"),
        new Effect("Clear a hotbar slot", "clear_hotbar"),
        new Effect("Shuffle the hotbar", "hotbar_shuffle"),
        new Effect("Steal a battery", "take_battery"),
        new Effect("Steal some equipment", "take_equipment"),
        new Effect("Kill bad things", "kill_enemies"),
        new Effect("Go back home", "respawn_player"),
        new Effect("Crafted Roulette", "advanced_roulette"),
        new Effect("Put your name on the map", "custom_beacon"),
        new Effect("Random Mouse Sensitivity (15 seconds)", "random_mouse"),
        new Effect("Invert Controls (60 seconds)", "invert_controls"),
        new Effect("Disable Controls (10 seconds)", "disable_controls"),
        new Effect("Random FOV (60 seconds)", "random_fov"),
        new Effect("Be careful Robin (60 seconds)", "ohko"),
        new Effect("Go REALLY fast (60 seconds)", "go_really_fast"),
    };
}
