using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using LiteDB;
using MEC;
using System;
using System.Collections.Generic;
using static ColorTag.Data;

namespace ColorTag
{
    public class Plugin : Plugin<Config>
    {
        public override string Prefix => "ColorTag";
        public override string Name => "ColorTag";
        public override string Author => "angelseraphim.";
        public static Plugin plugin;
        public LiteDatabase db { get; set; }
        public IEnumerator<float> ChangeColor(Player player)
        {

            int NowColor = 0;
            while(player.IsConnected) 
            {
                yield return Timing.WaitForSeconds(1f);
                try
                {
                    if (!Extensions.GetPlayer(player.UserId, out PlayerInfo info))
                        continue;
                    player.RankColor = info.colors[NowColor];
                    if (info.colors.Count - 1 > NowColor)
                    {
                        NowColor++;
                        continue;
                    }
                    NowColor = 0;
                }
                catch (Exception ex)
                {
                    //Log.Error(ex);
                    NowColor = 0;
                }
            }
            yield break;
        }

        public override void OnEnabled()
        {
            plugin = this;
            db = new LiteDatabase($"/root/.config/EXILED/Configs/DataBase/ColorSetting.db");
            Exiled.Events.Handlers.Player.Verified += OnVerifed;
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            plugin = null;
            db.Dispose();
            db = null;
            Exiled.Events.Handlers.Player.Verified -= OnVerifed;
            base.OnDisabled();
        }
        private void OnVerifed(VerifiedEventArgs ev)
        {
            if (!Extensions.GetPlayer(ev.Player.UserId, out PlayerInfo info))
                return;
            if (ev.Player.GroupName == null)
                return;
            Timing.RunCoroutine(ChangeColor(ev.Player));
        }
        public string ShowColors()
        {
            return "Aviable colors: <color=#FF96DE>pink</color>, <color=#C50000>red</color>, <color=#944710>brown</color>, <color=#A0A0A0>silver</color>, <color=#32CD32>light_green</color>, <color=#DC143C>crimson</color>, <color=#00B7EB>cyan</color>, <color=#00FFFF>aqua</color>, <color=#FF1493>deep_pink</color>, <color=#FF6448>tomato</color>, <color=#FAFF86>yellow</color>, <color=#FF0090>magenta</color>, <color=#4DFFB8>blue_green</color>, <color=#FF9966>orange</color>, <color=#BFFF00>lime</color>, <color=#228B22>green</color>, <color=#50C878>emerald</color>, <color=#960018>carmine</color>, <color=#727472>nickel</color>, <color=#98FB98>mint</color>, <color=#4B5320>army_green</color>, <color=#EE7600>pumpkin</color>";
        }
    }
}
