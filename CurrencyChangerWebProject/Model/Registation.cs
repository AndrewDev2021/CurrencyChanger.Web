namespace CurrencyChangerWebProject.Model
{
    public class Registation
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Password { get; set; }
        public int ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }
    }
}
