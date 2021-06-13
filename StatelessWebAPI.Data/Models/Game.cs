namespace StatelessWebAPI.Data.Models
{
    public class Game
    {
        public int Id { get; set; }

        public State State { get; set; }

        public int UserId { get; set; }
    }
}