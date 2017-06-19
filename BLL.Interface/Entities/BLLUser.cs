using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// This class represents a user on Business Logic Layer.
    /// </summary>
    public class BllUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? ProfileId { get; set; }
        public BllProfile Profile { get; set; }
    }
}
