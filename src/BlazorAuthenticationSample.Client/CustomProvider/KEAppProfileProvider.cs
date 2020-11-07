using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAuthenticationSample.Client.CustomProvider
{
    public interface IKEAppProfileProvider
    {
        Task<bool> CheckRole(string userId);
    }
    public class KEAppProfileProvider : IKEAppProfileProvider
    {
        public async Task<bool> CheckRole(string userId)
        {
            return await Task.FromResult(true);
        }



    }
}
