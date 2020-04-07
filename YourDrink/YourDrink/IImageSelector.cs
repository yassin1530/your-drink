using System;
using System.IO;
using System.Threading.Tasks;

namespace YourDrink
{
    public interface IImageSelector
    {
        Task<Stream> GetImageStreamAsync();
    }
}
