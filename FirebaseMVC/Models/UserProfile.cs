using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ForkToFit.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string FirebaseUserId { get; set; }

        public DateTime DateCreated { get; set; }

        public int BmrInfo { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public int Age { get; set; }

        public string Sex { get; set; }

    }
}
