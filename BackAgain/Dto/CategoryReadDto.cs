using BackAgain.Model;
using System.Collections.Generic;

namespace BackAgain.Dto
{
    public class CategoryReadDto
    {

        public string Id { get; set; }

        public string MenuId { get; set; }

        public string Name { get; set; }

        public bool Display { get; set; }

        public List<MenuItemReadDto> Items { get; set; }
    }
}