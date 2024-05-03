namespace OeuilDeSauron.Domain.Identity
{
    public class ChangePassword
    {
        public string UserId { get; set; }

        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

        public string PasswordConfirm { get; set; }
    }
}
