using BackAgain.Model;
using System.Collections.Generic;

namespace BackAgain.Dto
{
    public class MenuItemReadDto
    {
        public string Id { get; set; }

        public string CategoryId { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool Display { get; set; }

        public string Code { get; set; }

        public bool HasOptions { get; set; }

        public List<ItemExtraReadDto> ItemExtras { get; set; }

        public List<ItemOptionReadDto> ItemOptions { get; set; }
    }
}