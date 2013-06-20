using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class CreateGameCommand
    {
        [Required]
        public int RuleId { get; set; }
        [Required]
        public int PlayerOneId { get; set; }
        [Required]
        public int PlayerTwoId { get; set; }
    }
}
