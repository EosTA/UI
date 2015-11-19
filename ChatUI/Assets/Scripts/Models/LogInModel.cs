namespace Assets.Scripts.Models
{
    public class LogInModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public override string ToString()
        {
            return string.Format("username={0}&password={1}&grant_type=password", this.Email, this.Password);
        }
    }
}
