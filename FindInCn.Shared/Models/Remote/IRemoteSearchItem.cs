using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindInCn.Shared.Models.Remote
{
    public interface IRemoteSearchItem
    {
        string Name { get; }

        string Url { get; }

        //string StoreName { get; }

        //string StoreUrl { get; }

        string ImageUrl { get; }

        decimal Price { get; }

        string PriceString { get; }

        string Currency { get; }

        //feedback
    }
}
