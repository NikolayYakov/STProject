namespace STProject.DTOs
{
    public class UserDTO
    {
        public UserDTO(string email,string username)
        {
            Email = email;
            Username = username;
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public int TotalUpvotes { get; set; }
        public int TotalDownvotes { get; set; }
    }
}
