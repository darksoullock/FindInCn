﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote.Shops
{
    public class GenericSearchItem : IRemoteSearchItem
    {
        public string Name { get; set; }

        public decimal MinPrice { get; set; }

        public decimal MaxPrice { get; set; }

        public string PriceString { get; set; }
        
        public string Currency { get; set; }

        public string ImageUrl { get; set; }

        public string Url { get; set; }

        public int ShopId { get; set; }
    }
}