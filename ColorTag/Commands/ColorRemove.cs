﻿using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using static System.Net.Mime.MediaTypeNames;
using static ColorTag.Data;

namespace ColorTag.Commands
{
    public class ColorRemove : ICommand
    {
        public string Command { get; } = "remove";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "remove colors";
        public bool SanitizeResponse => false;
        private List<string> AvailableColors = new List<string>() { "pink", "red", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato", "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint", "army_green", "pumpkin" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            string Text = string.Empty;
            List<string> colors = new List<string>();
            if (!Extensions.GetPlayer(player.UserId, out PlayerInfo inforemove))
            {
                response = "Your settings were not found, please use the (colortag set (colors)) command";
                return false;
            }
            List<string> alreadyUsedColorsinforemove = inforemove.colors;
            for (int i = 0; i < arguments.Count; i++)
            {
                if (!AvailableColors.Contains(arguments.At(i)))
                {
                    response = $"Invalid color!\n{arguments.At(i)} cant be removed\n{Plugin.plugin.ShowColors()}";
                    return false;
                }
                colors.Add(arguments.At(i));
            }
            foreach (var s in colors)
            {
                alreadyUsedColorsinforemove.Remove(s);
            }
            foreach (var s in alreadyUsedColorsinforemove)
            {
                Text += $"{s} ";
            }
            inforemove.colors = alreadyUsedColorsinforemove;
            Extensions.PlayerInfoCollection.Update(inforemove);
            response = $"Your colors updated!\nColors: {Text}";
            return true;
        }
    }
}
