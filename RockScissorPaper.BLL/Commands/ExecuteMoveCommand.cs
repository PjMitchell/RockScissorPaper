using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RockScissorPaper.Domain;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace RockScissorPaper.BLL
{
    [DataContract]
    public class ExecuteMoveCommand
    {
        [Required]
        [DataMember(IsRequired=true)]
        public int PlayerId { get; set; }

        [Required]
        [DataMember(IsRequired = true)]
        public int GameId { get; set; }

        public GameSelection Selection { get; set; }

    }
}
