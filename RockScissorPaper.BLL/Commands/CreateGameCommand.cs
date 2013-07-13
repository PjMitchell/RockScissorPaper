using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    [DataContract]
    public class CreateGameCommand
    {
        [Required]
        [DataMember(IsRequired = true)]
        public int RuleId { get; set; }
        [Required]
        [DataMember(IsRequired = true)]
        public int PlayerOneId { get; set; }
        [Required]
        [DataMember(IsRequired = true)]
        public int PlayerTwoId { get; set; }
    }
}
