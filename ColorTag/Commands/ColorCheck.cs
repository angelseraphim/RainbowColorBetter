namespace ColorTag.Commands
{
    using System;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;

    using static ColorTag.Data;

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
                response = Plugin.plugin.Translation.DontHavePermissions.Replace("%permission%", Plugin.plugin.Config.AdminRequirePermission);
                return false;
            }
            if (arguments.Count != 1)
            {
                response = "Using: colortag check (UserID)";
                return false;
            }
            if (!Extensions.TryGetValue(arguments.At(0), out PlayerInfo info))
            {
                response = Plugin.plugin.Translation.OtherNotFound;
                return false;
            }
            string Text = $"{info.UserId}";
            foreach(string s in info.Colors)
            {
                Text += $"\n{s}";
            }
            response = Text;
            return true;
        }
    }
}
