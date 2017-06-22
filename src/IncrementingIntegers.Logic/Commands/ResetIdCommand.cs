namespace IncrementingIntegers.Logic.Commands
{
    public class ResetIdCommand
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public ResetIdCommand(string userId, int id)
        {
            UserId = userId;
            Id = id;
        }
    }
}