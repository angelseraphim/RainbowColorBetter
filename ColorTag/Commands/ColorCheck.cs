using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    public class ColorCheck : ICommand
    {
        public string Command { get; } = "check";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Check player settings";
        public bool SanitizeResponse => false;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (!player.CheckPermission(Plugin.plugin.Config.AdminRequirePermission))
            {
                response = $"You dont have permissions [{Plugin.plugin.Config.AdminRequirePermission}]";
                return false;
            }
            if (arguments.Count != 1)
            {
                response = "Using: colortag check (UserID)";
                return false;
            }
            if (!Extensions.GetPlayer(arguments.At(0), out PlayerInfo info))
            {
                response = "Player not found...";
                return false;
            }
            string Text = $"{info.ID}";
            foreach(string s in info.colors)
            {
                Text += $"\n{s}";
            }
            response = Text;
            return true;
        }
    }
}
