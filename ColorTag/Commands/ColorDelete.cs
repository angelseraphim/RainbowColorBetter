using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using static ColorTag.Data;

namespace ColorTag.Commands
{
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
                response = $"You dont have permissions [{Plugin.plugin.Config.AdminRequirePermission}]";
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
                    Extensions.DeleteAll();
                    response = "Date base has been killed";
                    return true;
                default:
                    if (!Extensions.GetPlayer(arguments.At(0), out PlayerInfo info))
                    {
                        response = "Player not found...";
                        return false;
                    }
                    Extensions.DeletePlayer(arguments.At(0));
                    response = $"Player with ID {arguments.At(0)} has been deleted";
                    return true;
            }
        }
    }
}
