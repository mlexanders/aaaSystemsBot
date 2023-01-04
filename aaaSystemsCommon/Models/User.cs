using System.Reflection;
using aaaSystemsCommon.Models.Difinitions;

namespace aaaSystemsCommon.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Additional { get; set; }
        public Role Role{ get; set; }
    }
}
