using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class CreatePlayerCommand
    {
        [Required]
        [StringLength(25)]
        public string PlayerName { get; set; }

        [Required]
        [StringLength(25)]
        public string IPAddress { get; set; }

    }
}
