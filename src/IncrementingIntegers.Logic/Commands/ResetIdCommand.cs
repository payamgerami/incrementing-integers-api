namespace IncrementingIntegers.Logic.Commands
{
    public class ResetIdCommand
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public ResetIdCommand(string email, int id)
        {
            Email = email;
            Id = id;
        }
    }
}