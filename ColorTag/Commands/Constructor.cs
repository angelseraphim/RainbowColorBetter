namespace ColorTag.Commands
{
    public class Constructor
    {
        public ColorAdd Add { get; set; } = new ColorAdd();
        public ColorHelp Help { get; set; } = new ColorHelp();
        public ColorRemove Remove { get; set; } = new ColorRemove();
        public ColorSet Set { get; set; } = new ColorSet();
    }
}
