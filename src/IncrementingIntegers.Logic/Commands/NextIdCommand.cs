namespace IncrementingIntegers.Logic.Commands
{
    public class NextIdCommand
    {
        public string UserId { get; set; }

        public NextIdCommand(string userId)
        {
            UserId = userId;
        }
    }
}