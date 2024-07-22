using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using static System.Net.Mime.MediaTypeNames;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    public class ColorSet : ICommand
    {
        public string Command { get; } = "set";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "set colors";
        public bool SanitizeResponse => false;
        private List<string> AvailableColors = new List<string>() { "pink", "red", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato", "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint", "army_green", "pumpkin" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            string Text = string.Empty;
            List<string> colors = new List<string>();
            if (!Plugin.plugin.Config.RequirGroups.Contains(player.GroupName))
            {
                response = "You dont have permissions";
                return false;
            }
            if (arguments.Count < 2)
            {
                response = "Usage: colortag set (colors)";
                return false;
            }
            if (arguments.Count > Plugin.plugin.Config.MaxColorLemit)
            {
                response = $"Max colors count: {Plugin.plugin.Config.MaxColorLemit}";
                return false;
            }
            for (int i = 0; i < arguments.Count; i++)
            {
                if (!AvailableColors.Contains(arguments.At(i)))
                {
                    response = $"Invalid color!\n{arguments.At(i)} cant be added\n{Plugin.plugin.ShowColors()}";
                    return false;
                }
                colors.Add(arguments.At(i));
            }
            foreach (var s in colors)
            {
                Text += $"{s} ";
            }
            if (!Extensions.GetPlayer(player.UserId, out PlayerInfo info))
            {
                Extensions.InsertPlayerAsync(player, colors);
                response = $"Your colors added!\nColors: {Text}";
                return true;
            }
            info.colors = colors;
            Extensions.PlayerInfoCollection.Update(info);
            response = $"Your colors added!\nColors: {Text}";
            return true;
        }
    }
}
