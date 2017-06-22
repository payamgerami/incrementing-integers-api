namespace IncrementingIntegers.Logic.Queries
{
    public class CurrentIdQuery
    {
        public string Email { get; set; }

        public CurrentIdQuery(string email)
        {
            Email = email;
        }
    }
}
