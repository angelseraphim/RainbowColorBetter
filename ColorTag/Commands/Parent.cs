using System;
using CommandSystem;
using Exiled.API.Features;

namespace ColorTag.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class Parent : ParentCommand
    {
        public Parent() => LoadGeneratedCommands();

        public override string Command { get; } = "colortag";

        public override string[] Aliases { get; } = { };

        public override string Description { get; } = "Add colors to tag";

        public Constructor Constructor { get; set; } = new Constructor();

        public sealed override void LoadGeneratedCommands()
        {
            Constructor constructor = Constructor;

            RegisterCommand(constructor.Add);
            RegisterCommand(constructor.check);
            RegisterCommand(constructor.delete);
            RegisterCommand(constructor.Remove);
            RegisterCommand(constructor.Set);
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (!Plugin.plugin.Config.RequirGroups.Contains(player.GroupName))
            {
                response = "You dont have permissions";
                return false;
            }
            response = "Usage: colortag (set/add/remove/check/delete) (colors)";
            return false;
        }
    }
}
