namespace ColorTag
{
    using ColorTag.Configs;

    using Exiled.API.Features;
    using Exiled.Events.EventArgs.Player;
    using Exiled.Permissions.Extensions;

    using LiteDB;

    using MEC;
    using System.Collections.Generic;

    public class Plugin : Plugin<Config, Translation>
    {
        public override string Prefix => "ColorTag";
        public override string Name => "ColorTag";
        public override string Author => "angelseraphim.";

        public readonly Dictionary<string, string> AvailableColors = new Dictionary<string, string>() 
        {
            {"pink", "#FF96DE"},
            {"red", "#C50000"},
            {"brown", "#944710"},
            {"silver", "#A0A0A0"},
            {"light_green", "#32CD32"},
            {"crimson", "#DC143C"},
            {"cyan", "#00B7EB"},
            {"aqua", "#00FFFF"},
            {"deep_pink", "#FF1493"},
            {"tomato", "#FF6448"},
            {"yellow", "#FAFF86"},
            {"magenta", "#FF0090"},
            {"blue_green", "#4DFFB8"},
            {"orange", "#FF9966"},
            {"lime", "#BFFF00"},
            {"green", "#228B22"},
            {"emerald", "#50C878"},
            {"carmine", "#960018"},
            {"nickel", "#727472"},
            {"mint", "#98FB98"},
            {"army_green", "#4B5320"},
            {"pumpkin", "#EE7600"}
        };

        public static Plugin plugin;
        public static Coroutines coroutines;
        public static GetDirectory directory;
        public static LiteDatabase database;

        public override void OnEnabled()
        {
            plugin = this;
            coroutines = new Coroutines();
            directory = new GetDirectory();
            database = new LiteDatabase(Config.DataPath.Replace("%config%", directory.GetParentDirectory(2)).Replace("%data%", $"ColorSetting{Server.Port}.db"));
            
            Exiled.Events.Handlers.Player.Verified += OnVerifed;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            plugin = null;
            coroutines = null;
            directory = null;
            database.Dispose();
            database = null;

            Exiled.Events.Handlers.Player.Verified -= OnVerifed;
            base.OnDisabled();
        }
        private void OnVerifed(VerifiedEventArgs ev)
        {
            if (!Extensions.Contains(ev.Player.UserId))
                return;
            if (string.IsNullOrEmpty(ev.Player.GroupName))
                return;
            if (!ev.Player.CheckPermission(Config.ColorRequirePermission))
                return;
            
            Timing.RunCoroutine(coroutines.ChangeColor(ev.Player));
        }
        public string ShowColors()
        {
            string content = string.Empty;
            foreach (var colors in AvailableColors)
            {
                content += $"<color={colors.Value}>{colors.Key}</color>" + "," + " ";
            }
            return $"Aviable colors: {content}";
        }
    }
}