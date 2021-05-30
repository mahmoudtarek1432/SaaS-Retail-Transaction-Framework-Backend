using BackAgain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Dto
{
    public class MenuReadDto
    {
        public string Id { get; set; }

        public List<CategoryReadDto> Categories { get; set; }
    }
}
