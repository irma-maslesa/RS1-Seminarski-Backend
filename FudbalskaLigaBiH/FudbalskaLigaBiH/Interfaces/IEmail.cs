using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FudbalskaLigaBiH.DTOs;

namespace FudbalskaLigaBiH.Interfaces
{
    public interface IEmail
    {
        Task Send(string emailAddress, string body, EmailOptionsDTO options);
    }
}
