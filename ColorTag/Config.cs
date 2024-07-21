using Exiled.API.Interfaces;
using System.Collections.Generic;

namespace ColorTag
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public List<string> RequirGroups { get; set; } = new List<string>() { "lvlB", "lvlC", "lvlD", "lvlE", "lvlW", "admin", "adminlvld", "glavb", "glav", "rukb", "ruk", "coo", "owner" };
    }
}