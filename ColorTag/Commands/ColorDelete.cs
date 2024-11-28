namespace ColorTag.Commands
{
    using System;

    using CommandSystem;

    using Exiled.API.Features;
    using Exiled.Permissions.Extensions;

    using static ColorTag.Data;

    public class ColorDelete : ICommand
    {
        public string Command { get; } = "delete";
        public string[] Aliases { get; } = { "del" };
        public string Description { get; } = "Delete player data";
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
                response = "Using: colortag delete (UserID/all)";
                return false;
            }

            switch (arguments.At(0))
            {
                case "all":
                    if (!player.CheckPermission(Plugin.plugin.Config.DropDataRequirePermission))
                    {
                        response = Plugin.plugin.Translation.DontHavePermissions.Replace("%permission%", Plugin.plugin.Config.DropDataRequirePermission);
                        return false;
                    }
                    Extensions.DeleteAll();
                    response = Plugin.plugin.Translation.KillDataBase;
                    return true;
                default:
                    if (!Extensions.TryGetValue(arguments.At(0), out PlayerInfo info))
                    {
                        response = Plugin.plugin.Translation.OtherNotFound;
                        return false;
                    }
                    Extensions.DeletePlayer(arguments.At(0));
                    response = Plugin.plugin.Translation.SuccessfullDeleted.Replace("%userid%", arguments.At(0));
                    return true;
            }
        }
    }
}
