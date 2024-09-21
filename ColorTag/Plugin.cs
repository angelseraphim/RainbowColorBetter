using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LiteDB;
using MEC;
using static ColorTag.Data;

namespace ColorTag
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "ColorTag";
        public override string Name => "ColorTag";
        public override string Author => "angelseraphim.";
        public static Plugin plugin;
        public static Coroutines coroutines;
        public static GetDirectory directory;
        public LiteDatabase db { get; set; }

        public override void OnEnabled()
        {
            plugin = this;
            coroutines = new Coroutines();
            directory = new GetDirectory();
            db = new LiteDatabase($"{directory.GetParentDirectory(2)}/ColorSetting{Server.Port}.db");
            Exiled.Events.Handlers.Player.Verified += OnVerifed;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            plugin = null;
            coroutines = null;
            directory = null;
            db.Dispose();
            db = null;
            Exiled.Events.Handlers.Player.Verified -= OnVerifed;
            base.OnDisabled();
        }
        private void OnVerifed(VerifiedEventArgs ev)
        {
            if (!Extensions.GetPlayer(ev.Player.UserId, out PlayerInfo info))
                return;
            if (string.IsNullOrEmpty(ev.Player.GroupName))
                return;
            
            Timing.RunCoroutine(coroutines.ChangeColor(ev.Player));
        }
        public string ShowColors()
        {
            return "Aviable colors: <color=#FF96DE>pink</color>, <color=#C50000>red</color>, <color=#944710>brown</color>, <color=#A0A0A0>silver</color>, <color=#32CD32>light_green</color>, <color=#DC143C>crimson</color>, <color=#00B7EB>cyan</color>, <color=#00FFFF>aqua</color>, <color=#FF1493>deep_pink</color>, <color=#FF6448>tomato</color>, <color=#FAFF86>yellow</color>, <color=#FF0090>magenta</color>, <color=#4DFFB8>blue_green</color>, <color=#FF9966>orange</color>, <color=#BFFF00>lime</color>, <color=#228B22>green</color>, <color=#50C878>emerald</color>, <color=#960018>carmine</color>, <color=#727472>nickel</color>, <color=#98FB98>mint</color>, <color=#4B5320>army_green</color>, <color=#EE7600>pumpkin</color>";
        }
    }
}