
using System.ComponentModel.DataAnnotations;

namespace USERS.API.Models
{
    public class PlayerData
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Point { get; set; }

        public PlayerData(int userId, int point)
        {
            UserId = userId;
            Point = point;
        }
    }
}