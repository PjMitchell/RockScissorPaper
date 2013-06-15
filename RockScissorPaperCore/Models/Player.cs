
namespace RockScissorPaper.Domain
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public BotBase Bot { get; set; }
        public bool IsBot { get { return Bot != null; } }
    }
}