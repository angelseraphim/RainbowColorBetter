namespace ColorTag.Configs
{
    using System.ComponentModel;

    using Exiled.API.Interfaces;

    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public string DataPath { get; set; } = "%config%/%data%";
        public int MaxColorLimit { get; set; } = 5;

        [Description("Rights so that the player can change his colors")]
        public string ColorRequirePermission { get; set; } = "colortag.get";

        [Description("Rights to remove colors from a player")]
        public string AdminRequirePermission { get; set; } = "colortag.admin";
        [Description("Rights to delete data base")]
        public string DropDataRequirePermission { get; set; } = "colortag.kill";
        [Description("Interval for color change")]
        public float Interval { get; set; } = 1f;
    }
}