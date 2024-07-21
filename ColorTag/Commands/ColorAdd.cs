using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using static System.Net.Mime.MediaTypeNames;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    public class ColorAdd : ICommand
    {
        public string Command { get; } = "add";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "add color";
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
            if (!Extensions.GetPlayer(player.UserId, out PlayerInfo infoadd))
            {
                response = "Your settings were not found, please use the (colortag set (colors)) command";
                return false;
            }
            List<string> alreadyUsedColors = infoadd.colors;
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
                alreadyUsedColors.Add(s);
            }
            foreach (var s in alreadyUsedColors)
            {
                Text += $"{s} ";
            }
            infoadd.colors = alreadyUsedColors;
            Extensions.PlayerInfoCollection.Update(infoadd);
            response = $"Your colors updated!\nColors: {Text}";
            return true;
        }
    }
}
