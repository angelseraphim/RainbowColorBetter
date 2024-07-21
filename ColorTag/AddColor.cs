using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using static ColorTag.Data;

namespace ColorTag
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class AddToDB : ICommand
    {
        public string Command { get; } = "colortag";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Add colors to tag";
        public bool SanitizeResponse => false;
        private List<string> AviableColors = new List<string>() { "pink", "red", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato", "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint", "army_green", "pumpkin" };
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
            switch (arguments.At(0))
            {
                case "set":
                    if(arguments.Count < 2)
                    {
                        response = "Usage: colortag set (colors)";
                        return false;
                    }
                    for (int i = 1; i < arguments.Count; i++)
                    {
                        if (!AviableColors.Contains(arguments.At(i)))
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
                case "add":
                    if (!Extensions.GetPlayer(player.UserId, out PlayerInfo infoadd))
                    {
                        response = "Your settings were not found, please use the (colortag set (colors)) command";
                        return false;
                    }
                    List<string> alreadyUsedColors = infoadd.colors;
                    for (int i = 1; i < arguments.Count; i++)
                    {
                        if (!AviableColors.Contains(arguments.At(i)))
                        {
                            response = $"Invalid color!\n{arguments.At(i)} cant be added\n{Plugin.plugin.ShowColors()}";
                            return false;
                        }
                        colors.Add(arguments.At(i));
                    }
                    foreach(var s in colors)
                    {
                        alreadyUsedColors.Add(s);
                    }
                    foreach(var s in alreadyUsedColors)
                    {
                        Text += $"{s} ";
                    }
                    infoadd.colors = alreadyUsedColors;
                    Extensions.PlayerInfoCollection.Update(infoadd);
                    response = $"Your colors updated!\nColors: {Text}";
                    return true;
                case "remove":
                    if (!Extensions.GetPlayer(player.UserId, out PlayerInfo inforemove))
                    {
                        response = "Your settings were not found, please use the (colortag set (colors)) command";
                        return false;
                    }
                    List<string> alreadyUsedColorsinforemove = inforemove.colors;
                    for (int i = 1; i < arguments.Count; i++)
                    {
                        if (!AviableColors.Contains(arguments.At(i)))
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
                default:
                    response = "Usage: colortag (set/add/remove) (colors)";
                    return false;
            }
        }
    }
}
