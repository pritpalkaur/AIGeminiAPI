namespace API.DTOs.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int TenantId { get; set; }
        public string Email { get; set; }        // Contact email
        public string Role { get; set; }         // Optional: Admin/User/etc.
        public DateTime CreatedAt { get; set; }  // When the user was created
        public bool IsActive { get; set; }       // Status flag
    }
}
