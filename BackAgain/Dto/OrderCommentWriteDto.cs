using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{

    public class OrderCommentWriteDto
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public string Comment { get; set; }

        public DateTime Date { get; set; }
    }
}
