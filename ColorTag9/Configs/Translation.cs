namespace ColorTag.Configs
{
    using Exiled.API.Interfaces;

    public class Translation : ITranslation
    {
        public string DontHavePermissions { get; set; } = "You dont have permissions %permission%";
        public string NotFoundInDataBase { get; set; } = "Your settings were not found, please use the (colortag set (colors)) command";
        public string OtherNotFound { get; set; } = "Player not found";
        public string ColorLimit { get; set; } = "Max colors count: %limit%";
        public string InvalidColor { get; set; } = "Invalid color!\n%arg% cant be added\n%colors%";
        public string KillDataBase { get; set; } = "Date base has been killed";
        public string Successfull { get; set; } = "Your colors updated!\nColors: %current%";
        public string SuccessfullForNextRound { get; set; } = "Your colors added!\nColor will be loaded in the next round\nColors: %current%";
        public string SuccessfullDeleted { get; set; } = "Player with ID %userid% has been deleted";

    }
}
