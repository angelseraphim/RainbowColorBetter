namespace ColorTag.Commands
{
    using System;
    using System.Collections.Generic;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;

    using static ColorTag.Data;

    public class ColorSet : ICommand
    {
        public string Command { get; } = "set";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Set colors";
        public bool SanitizeResponse => false;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            string Text = string.Empty;

            List<string> colors = new List<string>();

            if (!player.CheckPermission(Plugin.plugin.Config.ColorRequirePermission))
            {
                response = Plugin.plugin.Translation.DontHavePermissions.Replace("%permission%", Plugin.plugin.Config.ColorRequirePermission);
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Usage: colortag set (colors)";
                return false;
            }

            int count = arguments.Count;
            if (Plugin.plugin.Config.GroupColorLimit.TryGetValue(player.GroupName, out int limit))
            {
                if (count > limit)
                {
                    response = Plugin.plugin.Translation.ColorLimit.Replace("%limit%", limit.ToString());
                    return false;
                }
            }
            else if (count > Plugin.plugin.Config.DefaultColorLimit)
            {
                response = Plugin.plugin.Translation.ColorLimit.Replace("%limit%", Plugin.plugin.Config.DefaultColorLimit.ToString());
                return false;
            }

            foreach (string arg in arguments)
            {
                if (!Plugin.plugin.AvailableColors.ContainsKey(arg))
                {
                    response = Plugin.plugin.Translation.InvalidColor.Replace("%arg%", arg).Replace("%colors%", Plugin.plugin.ShowColors());
                    return false;
                }
                colors.Add(arg);
            }

            foreach (var s in colors)
            {
                Text += $"{s} ";
            }
            if (!Extensions.TryGetValue(player.UserId, out PlayerInfo info))
            {
                Extensions.InsertPlayerAsync(player, colors);
                response = Plugin.plugin.Translation.SuccessfullForNextRound.Replace("%current%", Text);
                return true;
            }
            info.Colors = colors;
            Extensions.PlayerInfoCollection.Update(info);
            response = Plugin.plugin.Translation.Successfull.Replace("%current%", Text);
            return true;
        }
    }
}
