namespace ColorTag.Commands
{
    public class Constructor
    {
        public ColorAdd Add { get; set; } = new ColorAdd();
        public ColorCheck check { get; set; } = new ColorCheck();
        public ColorDelete delete { get; set; } = new ColorDelete();
        public ColorRemove Remove { get; set; } = new ColorRemove();
        public ColorSet Set { get; set; } = new ColorSet();
    }
}
