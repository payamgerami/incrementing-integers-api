namespace IncrementingIntegers.Logic.Commands
{
    public class NextIdCommand
    {
        public string Email { get; set; }

        public NextIdCommand(string email)
        {
            Email = email;
        }
    }
}
