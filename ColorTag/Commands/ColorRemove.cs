namespace ColorTag.Commands
{
    using System;
    using System.Collections.Generic;

    using CommandSystem;

    using Exiled.API.Features;

    using static ColorTag.Data;

    public class ColorRemove : ICommand
    {
        public string Command { get; } = "remove";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "Remove colors";
        public bool SanitizeResponse => false;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            string Text = string.Empty;

            List<string> colors = new List<string>();

            if (!Extensions.TryGetValue(player.UserId, out PlayerInfo inforemove))
            {
                response = Plugin.plugin.Translation.NotFoundInDataBase;
                return false;
            }

            List<string> alreadyUsedColorsinforemove = inforemove.Colors;

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
                alreadyUsedColorsinforemove.Remove(s);
            }

            foreach (var s in alreadyUsedColorsinforemove)
            {
                Text += $"{s} ";
            }

            inforemove.Colors = alreadyUsedColorsinforemove;
            Extensions.PlayerInfoCollection.Update(inforemove);
            response = Plugin.plugin.Translation.Successfull;
            return true;
        }
    }
}
