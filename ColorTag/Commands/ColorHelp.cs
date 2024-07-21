using System;
using System.Collections.Generic;
using CommandSystem;

namespace ColorTag.Commands
{
    public class ColorHelp : ICommand
    {
        public string Command { get; } = "help";
        public string[] Aliases { get; } = { };
        public string Description { get; } = "help command";
        public bool SanitizeResponse => false;
        private List<string> AvailableColors = new List<string>() { "pink", "red", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato", "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint", "army_green", "pumpkin" };
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = "";
            return true;
        }
    }
}
