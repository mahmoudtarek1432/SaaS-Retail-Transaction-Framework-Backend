using System.Collections.Generic;

namespace BackAgain.Dto
{
    public class OrderItemWriteDto
    {

        public string OrderId { get; set; }

        public string ItemId { get; set; }

        //if null then no options were picked
        public string ItemOptionId { get; set; }

        public int Quantity { get; set; }

        public List<OrderItemExtraWriteDto> OrderExtras { get; set; }
    }
}