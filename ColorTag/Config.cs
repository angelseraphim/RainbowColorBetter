using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ColorTag
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public int MaxColorLimit { get; set; } = 5;

        [Description("Rights so that the player can change his colors")]
        public string ColorRequirePermission = "colortag.get";

        [Description("Rights to remove colors from a player")]
        public string AdminRequirePermission = "colortag.admin";
    }
}